using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public Character[] characters;
    void Awake()
    {
        foreach (var item in characters)
        {

        }
    }

   public Character GetCharacter(string name)
    {
        Character thisCharacter = Array.Find(characters, character => character.name == name);
        if (thisCharacter == null)
        {
            Debug.Log("Nie ma takiego obiektu jak: " + name);
            return null;
        }
        return thisCharacter;
    }
}
