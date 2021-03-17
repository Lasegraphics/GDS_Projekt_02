using UnityEngine;
using System;
using System.Collections.Generic;
using GridPack.Pathfinding.DataStructs;
using GridPack.Units;

namespace GridPack.Cells
{
    public abstract class Cell : MonoBehaviour, IGraphNode, IEquatable<Cell>
    {
        //Klasa zawiera reprezentacje pojedyńczej komórki 
       [HideInInspector]
        [SerializeField]
        private Vector2 _offsetCoord; 
        //Pozycja komórki w scenie
        public Vector2 OffsetCoord {get {return _offsetCoord;} set {_offsetCoord = value;}}
        //Sprawdza czy na komórce cos się znajduje
        public bool IsTaken; 
        public bool IsEffected; 
        //Koszt ruchu jednoski 
        public float MovementCost  = 1;

        public Unit CurrentUnit {get; set;}
        //Wykrywa klik na komórkę 
        public event EventHandler CellClicked; 
        //Podświetla komórkę gdy najezdza się kursorem
        public event EventHandler CellHighlighted; 
        //Zdarzenie jest wywoływane gdy kursor opuści komórkę
        public event EventHandler CellDehighlighted; 

        //Metody on mouse dla poszczególnych zdarzeń 

        protected virtual void OnMouseEnter()
        {
            if(CellHighlighted != null)
                CellHighlighted?.Invoke(this, new EventArgs());
        }

        protected virtual void OnMouseExit()
        {
            if(CellDehighlighted != null)
                CellDehighlighted?.Invoke(this, new EventArgs());
        }
        void OnMouseDown()
        {
            if (CellClicked != null)
                CellClicked?.Invoke(this, new EventArgs());
        }
        //Metoda zwraca dystans do komórki podanej jako parametr
        public abstract int GetDistance(Cell other);
        //Metoda zwraca komórki sąsiadujące z biezącą komórką z listy komórek podanej jako parametr
        public abstract List<Cell> GetNeighbours(List<Cell> cells);
        //Metoda zwraca fizyczne wymiary komórki.
        public abstract Vector3 GetCellDimensions();
        //Metoda oznacza komórkę, aby dać uzytkownikowi wskazówkę o tym, ze wybrana jednostka moze tam dotrzeć  
        public abstract void MarkAsReachable();
        //Metoda oznacza komórkę jako część trasy
        public abstract void MarkAsPath();
        //Metoda oznacza komórkę jako podświetloną w momencie kiedy kursor znajduje się nad komórką 
        public abstract void MarkAsHighlighted();
        //Metoda zwraca komórkę do domyślnego stanu 
        public abstract void UnMark();

        //Pobiera dystans do punktu B 
        public int GetDistance(IGraphNode other)
        {
            return GetDistance(other as Cell);
        } 
        //Metoda sprawdza równość koordynatów 
        public virtual bool Equals (Cell other)
        {
            return (OffsetCoord.x == other.OffsetCoord.x && OffsetCoord.y == other.OffsetCoord.y);
        }

        public override bool Equals(object other)
        {
            if(!(other is Cell))
                return false; 
            return Equals(other as Cell); 
        }
        //Indywidualny kod Hash dla pojedyńczej komórki 
        
        public override int GetHashCode()
        {
            
            int hash = 23; 
            hash = (hash * 37) + (int)OffsetCoord.x;
            hash = (hash * 37) + (int)OffsetCoord.y;
            return hash; 
            
        }
        
        //Metoda klonowania wratości do nowych pól 

        public abstract void CopyFields(Cell newCell);
    }
}