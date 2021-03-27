using System.Collections;
using System.Collections.Generic;
using GridPack.Units;
using UnityEngine;

public class FieldParameters : MonoBehaviour
{
    public int RandomHitPercent; 
    public int HitSpikeParameter; 
    public int HealTempleParameter; 

    private Unit GetHeal; 
    private Unit GetSpike; 
    private Unit GetRandom; 
    void Start()
    {
        HealTempleParameter = GetHeal.HealTempleParameterUnit;
        HitSpikeParameter = GetSpike.HitSpikeParameterUnit;
        RandomHitPercent = GetRandom.RandomHitPercentUnit; 

    }
}
