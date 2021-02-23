using GridPack.Grid;
using UnityEngine;

namespace GridPack.Players
{
    public abstract class Player : MonoBehaviour
    {
        public int PlayerNumber; 
        public abstract void Play(CellGrid cellGrid);
    }
}