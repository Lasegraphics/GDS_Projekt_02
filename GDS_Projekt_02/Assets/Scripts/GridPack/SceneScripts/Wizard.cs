using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;
using GridPack.Units;
using GridPack.Cells;

namespace GridPack.SceneScripts
{
    public class Wizard : Entity
    {   
        protected override void AttackActionPerformed(float actionCost)
        {
            ActionPoints -= actionCost; 

            if(ActionPoints == 0)
            {
                MovementPoints = 0; 
                SetState(new UnitStateMarkedAsFinished(this));
                Debug.Log("ActionPoints = 0 You cannot attack");   
            }
        }

        public override bool IsUnitAttackable(Unit other, Cell sourceCell)
        {
            if(AttackRange == 1)
            {
                return sourceCell.GetDistance(other.Cell) == AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.x == other.Cell.x
                && other.blockChecker == false 
                || sourceCell.GetDistance(other.Cell) == AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.y == other.Cell.y
                && other.blockChecker == false 
                || sourceCell.GetDistance(other.Cell) == AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.z == other.Cell.z
                && other.blockChecker == false ;
            }
            else
            {
                return sourceCell.GetDistance(other.Cell) < AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.x == other.Cell.x
                && other.blockChecker == false 
                || sourceCell.GetDistance(other.Cell) < AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.y == other.Cell.y
                && other.blockChecker == false 
                || sourceCell.GetDistance(other.Cell) < AttackRange
                && sourceCell.GetDistance(other.Cell) > MinAttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1
                && sourceCell.z == other.Cell.z
                && other.blockChecker == false; 
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

