using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;

namespace GridPack.SceneScripts
{
     public class Rogue : Entity
    {
      /*
        public override void OnTurnEnd()
        {
            if(HitPoints > 75)
            {
              HitPoints = 75;
            }
            catchedPaths = null; 
            Buffs.FindAll(b =>  b.Duration == 0).ForEach(b => {b.Undo(this);});
            Buffs.RemoveAll(b => b.Duration ==0);
            Buffs.ForEach(b => { b.Duration--; });

            SetState(new UnitStateNormal(this)); 
        }
        */
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
                MovementPoints = 3;//+= 1;

              //  this.UnMark();
            }
        }

    }
}

