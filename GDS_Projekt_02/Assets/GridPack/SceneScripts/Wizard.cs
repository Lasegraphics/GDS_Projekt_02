using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;

namespace GridPack.SceneScripts
{
    public class Wizard : ArmoredEntity
    {   
        protected override void AttackActionPerformed(float actionCost)
        {
            
            ActionPoints -= actionCost; 
            IsIgnored = true; 

            
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

    }
}

