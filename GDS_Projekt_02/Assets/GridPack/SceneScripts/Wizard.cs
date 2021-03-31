using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;
using GridPack.Units;
using GridPack.Cells;

namespace GridPack.SceneScripts
{
    public class Wizard : ArmoredEntity
    {   
        protected override void AttackActionPerformed(float actionCost)
        {
            
            ActionPoints -= actionCost; 
            //IsIgnored = true; 

            
            if(ActionPoints == 0)
            {
                MovementPoints = 0; 
                SetState(new UnitStateMarkedAsFinished(this));
               // EndTrn.EndTurn();
                Debug.Log("ActionPoints = 0 You cannot attack");   
            }

           /* if (MovementPoints == 0)
            {
                SetState(new UnitStateMarkedAsFinished(this));
            }
            */

        }

         public override bool IsUnitAttackable(Unit other, Cell sourceCell)
        {
        
            if(AttackRange == 1)
            {
                return sourceCell.GetDistance(other.Cell) == AttackRange
                && sourceCell.GetDistance(other.Cell) > 2
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1; 
            }
            else
            {
                return sourceCell.GetDistance(other.Cell) < AttackRange
                && sourceCell.GetDistance(other.Cell) > 2 
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1; 
            }
            
         
        }

        public override bool UnitIsntAttackable(Unit other, Cell sourceCell)
        {
           return sourceCell.GetDistance(other.Cell) >= AttackRange
            && sourceCell.GetDistance(other.Cell) > 2 
            && other.PlayerNumber != PlayerNumber
            && ActionPoints <= 1; 
        }
           

    }
}

