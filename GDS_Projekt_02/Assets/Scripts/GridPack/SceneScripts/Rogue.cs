using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units.UnitStates;

namespace GridPack.SceneScripts
{
  public class Rogue : Entity
  {
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
      }
      
      if(ActionPoints == 0) //&& MovementPoints == 0)
      {
        MovementPoints = 3;//+= 1;
      }
    }
  }
}

