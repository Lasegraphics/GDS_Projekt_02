using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Cells;
using GridPack.Pathfinding.Algorithms;
using GridPack.Units.UnitStates;
using GridPack.Grid; 
using GridPack.Grid.GridStates; 
using GridPack.SceneScripts;
using GridPack.Units; 

public class FieldParameters : MonoBehaviour
{
    public int HealValueTemple;
    public int HitValueSpikesUnit;

    private Unit getTempleHeal; 

    void Initialize()
    {
       HealValueTemple = getTempleHeal.HealValueTempleUnit;
    }
}
