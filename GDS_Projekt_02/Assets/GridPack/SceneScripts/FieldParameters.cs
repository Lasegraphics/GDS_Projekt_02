using System.Collections;
using System.Collections.Generic;
using GridPack.Units;
using GridPack.Cells;
using GridPack.Grid.GridStates;
using GridPack.Grid;
using UnityEngine;

public class FieldParameters : MonoBehaviour
{
    public int RandomHitPercent; 
    public int HitSpikeParameter; 
    public int HealTempleParameter; 

    private Unit GetHeal;
    
    /*
    public FieldParameters(Unit unit)
    {
        GetHeal = unit; 
    }
    void Start()
    {
        GetHeal.UnMark();
    }
    */
}
