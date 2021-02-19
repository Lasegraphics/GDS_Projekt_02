using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Character
{
    public string name;
    public Color color;
    public int health;
    public int armor;
    public int attack;
    public int rangeMovment;
    public bool distanceAttack;
    public int minDistance;
    public int maxDistance;
    public enum Tag { Warrior,Tank,Mage,Thief,Archer };
    public Tag tag;

}
