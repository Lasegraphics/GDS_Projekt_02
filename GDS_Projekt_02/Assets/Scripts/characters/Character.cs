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
    public enum WeaknessFirst { None, Rock, Paper, Scissors, Quinto, Taaster };
    public enum WeaknessSecond { None, Rock, Paper, Scissors, Quinto, Taaster };

    
    public WeaknessFirst weaknessFirst;
    public WeaknessSecond weaknessSecond;
}
