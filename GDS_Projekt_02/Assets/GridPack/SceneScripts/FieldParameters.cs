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
    public int HitValueSpikes;
    public int randomPercentInForest; 

    private Unit getTempleHeal; 
    private Unit getSpikesHit;
    private Unit getRandomPercent; 

    void Initialize()
    {
       HealValueTemple = getTempleHeal.HealValueTempleUnit;
       HitValueSpikes = getSpikesHit.HitValueSpikesUnit;
       randomPercentInForest = getRandomPercent.RandomPercentHit; 
    }
}
