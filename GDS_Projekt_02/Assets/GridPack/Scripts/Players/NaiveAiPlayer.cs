using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GridPack.Cells;
using GridPack.Grid;
using GridPack.Grid.GridStates;
using GridPack.Units;
using UnityEngine;

namespace GridPack.Players
{
    public class NaiveAiPlayer : Player
    {
        private CellGrid _cellGrid;
        private System.Random _rnd;

        public NaiveAiPlayer()
        {
            _rnd = new System.Random();
        }

        public override void Play(CellGrid cellGrid)
        {
            cellGrid.CellGridState = new CellGridStateBlockInput(cellGrid);
            _cellGrid = cellGrid;

            StartCoroutine(Play());

        }
        private IEnumerator Play()
        {
            var myUnits = _cellGrid.Units.FindAll(u => u.PlayerNumber.Equals(PlayerNumber)).ToList();
            foreach (var unit in myUnits.OrderByDescending(u => u.Cell.GetNeighbours(_cellGrid.Cells).FindAll(u.IsCellTraversable).Count))
            {
                var enemyUnits = _cellGrid.Units.Except(myUnits).ToList();
                var unitsInRange = new List<Unit>();
                foreach (var enemyUnit in enemyUnits)
                {
                    if (unit.IsUnitAttackable(enemyUnit, unit.Cell))
                    {
                        unitsInRange.Add(enemyUnit);
                    }
                }
                if (unitsInRange.Count != 0)
                {
                    var index = _rnd.Next(0, unitsInRange.Count);
                    unit.AttackHandler(unitsInRange[index]);
                    yield return new WaitForSeconds(0.5f);
                    continue;
                }
                List<Cell> potentialDestinations = new List<Cell>();

                foreach (var enemyUnit in enemyUnits)
                {
                    potentialDestinations.AddRange(_cellGrid.Cells.FindAll(c => unit.IsCellMovableTo(c) && unit.IsUnitAttackable(enemyUnit, c)));
                }

                var notInRange = potentialDestinations.FindAll(c => c.GetDistance(unit.Cell) > unit.MovementPoints);
                potentialDestinations = potentialDestinations.Except(notInRange).ToList();

                if (potentialDestinations.Count == 0 && notInRange.Count != 0)
                {
                    potentialDestinations.Add(notInRange.ElementAt(_rnd.Next(0, notInRange.Count - 1)));
                }

                potentialDestinations = potentialDestinations.OrderBy(h => _rnd.Next()).ToList();
                List<Cell> shortestPath = null;
                foreach (var potentialDestination in potentialDestinations)
                {
                    var path = unit.FindPath(_cellGrid.Cells, potentialDestination);
                    if ((shortestPath == null && path.Sum(h => h.MovementCost) > 0) || shortestPath != null && (path.Sum(h => h.MovementCost) < shortestPath.Sum(h => h.MovementCost) && path.Sum(h => h.MovementCost) > 0))
                        shortestPath = path;

                    var pathCost = path.Sum(h => h.MovementCost);
                    if (pathCost > 0 && pathCost <= unit.MovementPoints)
                    {
                        unit.Move(potentialDestination, path);
                        while (unit.IsMoving)
                            yield return 0;
                        shortestPath = null;
                        break;
                    }
                    yield return 0;
                }

                if (shortestPath != null)
                {
                    foreach (var potentialDestination in shortestPath.Intersect(unit.GetAvailableDestinations(_cellGrid.Cells)).OrderByDescending(h => h.GetDistance(unit.Cell)))
                    {
                        var path = unit.FindPath(_cellGrid.Cells, potentialDestination);
                        var pathCost = path.Sum(h => h.MovementCost);
                        if (pathCost > 0 && pathCost <= unit.MovementPoints)
                        {
                            unit.Move(potentialDestination, path);
                            while (unit.IsMoving)
                                yield return 0;
                            break;
                        }
                        yield return 0;
                    }
                }

                foreach (var enemyUnit in enemyUnits)
                {
                    var enemyCell = enemyUnit.Cell;
                    if (unit.IsUnitAttackable(enemyUnit, unit.Cell))
                    {
                        unit.AttackHandler(enemyUnit);
                        yield return new WaitForSeconds(0.5f);
                        break;
                    }
                }
            }
            _cellGrid.EndTurn();
        }
    }
}