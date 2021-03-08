using System.Collections;
using GridPack.Cells;
using GridPack.Units;
using UnityEngine;
using System; 

namespace Scripts.characters
{
    
    public class CharacterController : MonoBehaviour
    {
        public Character [] characters;

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
}