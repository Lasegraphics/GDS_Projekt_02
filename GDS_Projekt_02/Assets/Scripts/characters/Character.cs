using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Character
{
    [Header("Statystki")]
    public string name;
    public Color color;
    public int health;
    public int armor;
    public int rangeMovment;
    

    [Header("Obrazenia")]
    [SerializeField] int attackMin;
    [SerializeField] int attackMax;
    public bool ignoreArmor;
    public bool distanceAttack;
    public int minDistance;
    public int maxDistance;

    public enum Tag { None, Tank, Warrior, Magic, Archer, Rogue };
    public int attack()
    {
       int dmg = Random.Range(attackMin, attackMax);
        return dmg;
    }
    public Tag weaknessFirst;
}
