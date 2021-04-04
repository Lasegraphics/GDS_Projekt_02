using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;

namespace GridPack.SceneScripts
{
     public class Rogue : Entity
     {

         [Header("Dla Rogue")] 
         [SerializeField]
         private int movmentPointsAfterAttack;

        protected override void AttackActionPerformed(float actionCost)
        {
            ActionPoints -= actionCost; 
            if(ActionPoints == 0)
            {
               // MovementPoints = 0; 
               // SetState(new UnitStateMarkedAsFinished(this));
               // EndTrn.EndTurn();
                Debug.Log("ActionPoints = 0 You cannot attack");   
            }

            if(MovementPoints == 0)
            {
                SetState(new UnitStateMarkedAsFinished(this));
              //  Cell.CurrentUnit.OnDestroyed();
            }


            // Tu jest ta zmienna dla mnie i Micha�a do robienia movementu Rogue'a. ~Wojtek
            if(ActionPoints == 0) //&& MovementPoints == 0)
            {
                MovementPoints = movmentPointsAfterAttack;//+= 1;

              //  this.UnMark();
            }
        }

    }
}

