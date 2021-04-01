using GridPack.Cells;
using GridPack.Units;
using GridPack.Grid;
using UnityEngine;
using System;

namespace GridPack.Grid.GridStates
{
    public class CellGridState
    {
        protected CellGrid _cellGrid; 
        //public CellGrid CellGrid; 


        protected CellGridState(CellGrid cellGrid)
        {
            _cellGrid = cellGrid; 
        }
        
        //Metoda jest wywoływana po kliknięciu komórki 
        public virtual void OnUnitClicked(Unit unit)
        {

        }

        //Metoda jest wywoływana w momencie wyjścia myszki z obszaru komórki 
        public virtual void OnCellDeselected(Cell cell)
        {
                cell.UnMark();       
        }

        //Metoda jest wywoływana w momencie kiedy kursor znajduje się w obszarze komórki 
        public virtual void OnCellSelected(Cell cell)
        {
            cell.MarkAsHighlighted();
        }

        //Metoda jest wywoływana w momencie kiedy komórka jest kliknięta
        public virtual void OnCellClicked(Cell cell)
        {
        }

        //Metoda jest wywoływana w momencie wejścia w stan
        public virtual void OnStateEnter()
        {
            foreach (var cell in _cellGrid.Cells)
            {
                cell.UnMark();
                
            }
        }

        //Metoda jest wywoływana w momencie wyjścia ze stanu 
        public virtual void OnStateExit()
        {
    
        }
    }
}