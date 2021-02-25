using GridPack.Grid;
using UnityEngine;

namespace GridPack.Players
{
    //Klasa reprezentuje uczestnika gry.
    public abstract class Player : MonoBehaviour
    {
        public int PlayerNumber; 

        //Metoda jest wywoływana kazdej tury. Pozwala graczowi na interakcję z jednostkami. 
        public abstract void Play(CellGrid cellGrid);
    }
}