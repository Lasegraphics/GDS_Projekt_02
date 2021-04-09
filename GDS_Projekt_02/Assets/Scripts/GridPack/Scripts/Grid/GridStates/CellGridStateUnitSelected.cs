using System.Collections.Generic;
using System.Linq;
using GridPack.Cells;
using GridPack.Units;
using GridPack.Units.UnitStates;
using UnityEngine; 

namespace GridPack.Grid.GridStates
{
    public class CellGridStateUnitSelected : CellGridState
    {
        private Unit _unit;
        private HashSet<Cell> _pathsInRange;
        private List<Unit> _unitsInRange;
        private List<Unit> _unitsMarkedInRange;
        private Cell _unitCell;
        private Cell anotherUnitCell; 
        private List<Cell> _currentPath;
        private List<Unit> unitsInX;
        private List<Unit> unitsInY;
        private List<Unit> unitsInZ;

        int FirstEnemycoordx;
        
    
        public CellGridStateUnitSelected(CellGrid cellGrid, Unit unit) : base(cellGrid)
        {
            _unit = unit;
            _pathsInRange = new HashSet<Cell>();
            _currentPath = new List<Cell>();
            _unitsInRange = new List<Unit>(0);
            _unitsMarkedInRange = new List<Unit>();
             unitsInX = new List<Unit>();
             unitsInY = new List<Unit>();
             unitsInZ = new List<Unit>();
        }

        public override void OnCellClicked(Cell cell)
        {
             _unitCell = _unit.Cell;
            if (_unit.IsMoving)
            {
                return;
            }

            if (cell.IsBlocked || !_pathsInRange.Contains(cell))
            {
                _cellGrid.CellGridState = new CellGridStateWaitingForInput(_cellGrid);
                return;
            }
            var path = _unit.FindPath(_cellGrid.Cells, cell);
            _unit.Move(cell, path);
            _cellGrid.CellGridState = new CellGridStateUnitSelected(_cellGrid, _unit);
            _unitCell.MarkAsPlayerEntity();
        }

        public override void OnUnitClicked(Unit unit)
        {
           _unit.UnMark();
           _unit.OnUnitDeselected();
           _cellGrid.CellGridState = new CellGridStateWaitingForInput(_cellGrid);
            if (unit.Equals(_unit) || _unit.IsMoving)
            {
                return;
            }
                
            if (_unitsInRange.Contains(unit) && !_unit.IsMoving)
            {             
                _unit.AttackHandler(unit);
                if (!_cellGrid.GameFinished)
                {
                    _cellGrid.CellGridState = new CellGridStateUnitSelected(_cellGrid, _unit);    /// KURWA DZIAŁA
                }
            }

            if (unit.PlayerNumber.Equals(_unit.PlayerNumber))
            {
                _cellGrid.CellGridState = new CellGridStateUnitSelected(_cellGrid, unit);
            }
        }

        public override void OnCellDeselected(Cell cell)
        {
            base.OnCellDeselected(cell);
            anotherUnitCell = _unit.Cell;
            

            foreach (var _cell in _currentPath)
            {    
                if (_pathsInRange.Contains(_cell))
                    _cell.MarkAsReachable();
                else
                   _unit.UnMark();
            }

            foreach (var unit in _unitsMarkedInRange)
            {
                unit.UnMark();
            }
            _unitsMarkedInRange.Clear();
            foreach (var unit in _unitsInRange)
            {
                _unitCell = unit.Cell; 
            
                if(_unit.Cell.x == _unitCell.x)
                {
                    unitsInX.Add(unit);
                    Debug.Log(unitsInX);
                    unitsInX.Count(); 
                    Debug.Log(unitsInX.Count() + "X");
                    //unit.blockChecker = false; 
                }
                
                if(_unit.Cell.y == _unitCell.y)
                {
                    unitsInY.Add(unit);
                    Debug.Log(unitsInY);
                    unitsInY.Count(); 
                    Debug.Log(unitsInY.Count() + "Y");
                    //unit.blockChecker = false; 
                }
                if(_unit.Cell.z == _unitCell.z)
                {
                    unitsInZ.Add(unit);
                    Debug.Log(unitsInZ);
                    unitsInZ.Count(); 
                    Debug.Log(unitsInZ.Count() + "Z");
                    //unit.blockChecker = false; 
                }
                foreach (var unitX in unitsInX)
                {
                    unitX.blockChecker = true;
                    unitsInX.First().blockChecker = false;
                    unitX.Cell.MarkAsEnemyEntity();  
                    
                }
                foreach (var unitY in unitsInY)
                {
                    unitY.blockChecker = true;
                    unitsInY.First().blockChecker = false;
                    unitY.Cell.MarkAsEnemyEntity();  
                    
                }    
                foreach (var unitZ in unitsInZ)
                {
                    unitZ.blockChecker = true;
                   // unitZ.blockChecker = true;
                    unitsInZ.First().blockChecker = false;
                    unitZ.Cell.MarkAsEnemyEntity();   
                }                 
                
                //_unitCell.MarkAsEnemyEntity();
                //unit.MarkAsReachableEnemy();    
            }
            unitsInX.Clear();
            unitsInY.Clear();
            unitsInZ.Clear();
            
            

            anotherUnitCell.MarkAsPlayerEntity();
        }

        public override void OnCellSelected(Cell cell)
        {
            base.OnCellSelected(cell);
            anotherUnitCell = _unit.Cell;
            if (!_pathsInRange.Contains(cell)) return;
            _currentPath = _unit.FindPath(_cellGrid.Cells, cell);
            foreach (var _cell in _currentPath)
            {
                _cell.MarkAsPath();
            }

            foreach (var unit in _unitsInRange)
            {
                unit.UnMark();      
            }
            foreach (var currentUnit in _cellGrid.Units)
            {
                if (_unit.IsUnitAttackable(currentUnit, cell))
                {       
                    _unitCell = currentUnit.Cell;
                    _unitCell.MarkAsEnemyEntity();
                    _unitsMarkedInRange.Add(currentUnit);
                }
                if (_unit.UnitIsntAttackable(currentUnit, cell))
                {
                    _unitCell = currentUnit.Cell;
                    _unitCell.UnMark();
                    _unitsMarkedInRange.Add(currentUnit);
                }
                anotherUnitCell.MarkAsPlayerEntity();
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _unit.OnUnitSelected();
            _unitCell = _unit.Cell;
            _pathsInRange = _unit.GetAvailableDestinations(_cellGrid.Cells);
            var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);
            foreach (var cell in cellsNotInRange)
            {
                cell.UnMark();
            }
            foreach (var cell in _pathsInRange)
            {
                cell.MarkAsReachable();
            }
            _unitCell.MarkAsPlayerEntity();
            if (_unit.ActionPoints <= 0) return;
            foreach (var currentUnit in _cellGrid.Units)
            {
                if (currentUnit.PlayerNumber.Equals(_unit.PlayerNumber))
                    continue;
                if (_unit.IsUnitAttackable(currentUnit, _unit.Cell))
                {
                    _unitCell = currentUnit.Cell;
                    _unitCell.MarkAsEnemyEntity();

                    if(_cellGrid.IsSwitched == true)
                    {
                        Debug.Log(currentUnit);
                        currentUnit.HitPoints -= 1; 
                        Debug.Log("Zdrowie:" + currentUnit.HitPoints); 
                    }
                    _unitsInRange.Add(currentUnit);
                }
            }
            if (_unitCell.GetNeighbours(_cellGrid.Cells).FindAll(c => c.MovementCost <= _unit.MovementPoints).Count == 0
                && _unitsInRange.Count == 0)
                _unit.SetState(new UnitStateMarkedAsFinished(_unit));
        }
        public override void OnStateExit()
        {
            _unitCell = _unit.Cell;
            _unit.OnUnitDeselected();
            FirstEnemycoordx = 0;
            foreach (var unit in _unitsInRange)
            {
                if (unit == null) continue;
                unit.SetState(new UnitStateNormal(unit));
            }
            foreach (var cell in _cellGrid.Cells)
            {
                cell.UnMark();
            }
            _unitCell.MarkAsPlayerEntity();
           
        }
    }
}