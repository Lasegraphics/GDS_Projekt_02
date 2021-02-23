using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using GridPack.Cells;
using GridPack.Pathfinding.Algorithms;
using GridPack.Units.UnitStates;

namespace GridPack.Units
{
   [ExecuteInEditMode]
    public abstract class Unit : MonoBehaviour
    {
        Dictionary<Cell, List<Cell>> catchedPaths = null;  
        public event EventHandler UnitClicked; 
        public event EventHandler UnitSelected; 
        public event EventHandler UnitDeselected;
        public event EventHandler UnitHighlighted; 
        public event EventHandler UnitDehighlighted;
        public event EventHandler<AttackEventArgs> UnitAttacked;   
        public event EventHandler<AttackEventArgs> UnitDestroyed;   
        public event EventHandler<MovementEventArgs> UnitMoved;   

        public UnitState UnitState {get; set;} 
        public void SetState(UnitState state)
        {
            UnitState.MakeTransition(state);
        }

        public List<Buff> Buffs {get; set;}

        public int TotalHitPoints {get; private set;}
        public float TotalMovementPoints{get; private set;}
        public float TotalActionPoints {get; private set;}

        [SerializeField]
        [HideInInspector]

        private Cell cell; 
        public Cell Cell
        {
            get
            {
                return cell; 
            }
            set
            {
                cell = value; 
            }
        }

        public int HitPoints; 
        public int AttackRange;
        public int AttackFactor; 
        public int DefenceFactor; 

        [SerializeField]
        private float movementPoints; 

        public virtual float MovementPoints
        {
            get
            {
                return movementPoints; 
            }

            protected set 
            {
                movementPoints = value;    
            }
        }

        public float MovementAnimationSpeed; 
        [SerializeField]
        public float actionPoints = 1;
        public float ActionPoints
        {
            get
            {
                return actionPoints;
            }
            protected set
            {
                actionPoints = value; 
            }

        }

        public int PlayerNumber; 

        public bool IsMoving{get; set;}

        private static DijkstraPathfinding _pathfinder = new DijkstraPathfinding(); 
        private static IPathfinding _fallbackPathfinder = new AStarPathfinding();

        public virtual void Initialize()
        {
            Buffs = new List<Buff>();
            UnitState = new UnitStateNormal(this);

            TotalHitPoints = HitPoints; 
            TotalMovementPoints = MovementPoints;
            TotalActionPoints = ActionPoints; 
        }

        protected virtual void OnMouseDown()
        {
            if(UnitClicked != null)
            {
                UnitClicked.Invoke(this, new EventArgs());
            }
        }

        protected virtual void OnMouseEnter()
        {
            if(UnitHighlighted != null)
            {
                UnitHighlighted.Invoke(this, new EventArgs());

            }
        }

        protected virtual void OnMouseExit()
        {
            if(UnitDehighlighted != null)
            {
                UnitDehighlighted.Invoke(this, new EventArgs());

            }
        }

        public virtual void OnTurnStart()
        {
            MovementPoints = TotalMovementPoints;
            ActionPoints = TotalActionPoints;

            SetState(new UnitStateMarkedAsFriendly(this)); 
        }

        public virtual void OnTurnEnd()
        {
            catchedPaths = null; 
            Buffs.FindAll(b =>  b.Duration == 0).ForEach(b => {b.Undo(this);});
            Buffs.RemoveAll(b => b.Duration ==0);
            Buffs.ForEach(b => { b.Duration--; });

            SetState(new UnitStateNormal(this)); 
        }

        protected virtual void OnDestroyed()
        {
            Cell.IsTaken = false; 
            MarkAsDestroyed();
            Destroy(this.gameObject);
        }

        public virtual void OnUnitSelected()
        {
            SetState(new UnitStateMarkedAsSelected(this));
            if(UnitSelected != null)
            {
                UnitSelected.Invoke(this, new EventArgs());
            }
        }
        public virtual void OnUnitDeselected()
        {
            SetState(new UnitStateMarkedAsFriendly(this));
            if(UnitDeselected != null)
            {
                UnitDeselected.Invoke(this, new EventArgs());
            }
        }

        public virtual bool IsUnitAttackable(Unit other, Cell sourceCell)
        {
           return sourceCell.GetDistance(other.Cell) <= AttackRange
            && other.PlayerNumber != PlayerNumber
            && ActionPoints >= 1; 
        }

        public void AttackHandler(Unit unitToAttack)
        {
            if(!IsUnitAttackable(unitToAttack, Cell))
            {
                return; 
            }

            AttackAction attackAction = DealDamage(unitToAttack);
            MarkAsAttacking(unitToAttack); 
            unitToAttack.DefendHandler(this, attackAction.Damage); 
            AttackActionPerformed(attackAction.ActionCost);
        }

        protected virtual AttackAction DealDamage(Unit unitToAttack)
        {
            return new AttackAction(AttackFactor, 1f); 
        }

        protected virtual void AttackActionPerformed(float actionCost)
        {
            ActionPoints -= actionCost; 
            if(ActionPoints == 0)
            {
                MovementPoints = 0; 
                SetState(new UnitStateMarkedAsFinished(this));
            }
        }

        public void DefendHandler(Unit aggressor, int damage)
        {
            MarkAsDefending(aggressor); 
            int damageTaken = Defend(aggressor, damage);
            HitPoints -= damageTaken;  
            DefenceActionPerformed();

            if(UnitAttacked != null)
            {
                UnitAttacked.Invoke(this, new AttackEventArgs(aggressor, this, damage)); 
            }

            if(HitPoints <= 0)
            {
                if (UnitDestroyed != null)
                {
                    UnitDestroyed.Invoke(this, new AttackEventArgs(aggressor, this, damage));
                }
                OnDestroyed();
            }
        }

        protected virtual int Defend(Unit aggresor, int damage)
        {
            return Mathf.Clamp(damage - DefenceFactor, 1, damage);
        } 

        protected virtual void DefenceActionPerformed(){}
        
        public virtual void Move(Cell destinationCell, List<Cell> path)
        {
            var totalMovementCost = path.Sum(h => h.MovementCost);
            MovementPoints -= totalMovementCost;  
            Cell.IsTaken = false;
            Cell.CurrentUnit = null;
            Cell = destinationCell; 
            destinationCell.IsTaken = true; 
            destinationCell.CurrentUnit = this; 

            if(MovementAnimationSpeed > 0)
            {
                StartCoroutine(MovementAnimation(path));
            }
            else 
            {
                transform.position = Cell.transform.position; 
            }

            if(UnitMoved != null)
            {
                UnitMoved.Invoke(this, new MovementEventArgs(Cell, destinationCell, path));
            }

        }

        protected virtual IEnumerator MovementAnimation(List<Cell> path)
        {
            IsMoving = true; 
            path.Reverse();
            foreach (var cell in path)
            {
                Vector3 destination_pos = new Vector3(cell.transform.localPosition.x, cell.transform.localPosition.y, transform.localPosition.z);
                while (transform.localPosition != destination_pos)
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementAnimationSpeed);
                    yield return 0; 
                }
            }

            IsMoving = false; 
            OnMoveFinished();
        }

        protected virtual void OnMoveFinished(){}

        public virtual bool IsCellMovableTo(Cell cell)
        {
            return !cell.IsTaken; 
        }

        public virtual bool IsCellTraversable(Cell cell)
        {
            return !cell.IsTaken; 
        }

        public HashSet<Cell> GetAvailableDestinations(List<Cell> cells)
        {
            catchedPaths = new Dictionary<Cell, List<Cell>>(); 
            var paths = CachePaths(cells);
            foreach (var key in paths.Keys)
            {
                if(!IsCellMovableTo(key))
                {
                    continue; 
                }

                var path = paths[key];
                var pathCost = path.Sum(c => c.MovementCost);
                if(pathCost <= MovementPoints)
                {
                    catchedPaths.Add(key, path);
                }
            }
            return new HashSet<Cell>(catchedPaths.Keys);
        }

        private Dictionary<Cell, List<Cell>> CachePaths(List<Cell> cells)
        {
            var edges = GetGraphEdges(cells);
            var paths = _pathfinder.findAllPaths(edges, Cell);
            return paths; 
        }

        public List<Cell> FindPath(List<Cell>cells, Cell destination)
        {
            if(catchedPaths != null && catchedPaths.ContainsKey(destination))
            {
                return catchedPaths[destination]; 
            }
            else 
            {
                return _fallbackPathfinder.FindPath(GetGraphEdges(cells),Cell, destination);
            }
        }

        protected virtual Dictionary<Cell, Dictionary<Cell, float>> GetGraphEdges(List<Cell> cells)
        {
            Dictionary<Cell, Dictionary<Cell, float>> ret = new Dictionary<Cell, Dictionary<Cell, float>> ();
            foreach (var cell in cells)
            {
                if(IsCellTraversable(cell) || cell.Equals(Cell))
                {
                    ret[cell] = new Dictionary<Cell, float>();
                    foreach(var neighbour in cell.GetNeighbours(cells).FindAll(IsCellTraversable))
                    {
                        ret[cell][neighbour] = neighbour.MovementCost; 
                    }
                }
            }
            return ret; 
        }

        public abstract void MarkAsDefending(Unit aggressor);
        public abstract void MarkAsAttacking(Unit target); 
        public abstract void MarkAsDestroyed();
        public abstract void MarkAsFriendly();
        public abstract void MarkAsReachableEnemy();
        public abstract void MarkAsSelected();
        public abstract void MarkAsFinished(); 
        public abstract void UnMark();

        [ExecuteInEditMode]
        public void OnDestroy()
        {
            if(Cell != null)
            {
                Cell.IsTaken = false; 
            }
        }
    }
    
    public class AttackAction 
    {
        public readonly int Damage; 
        public readonly float ActionCost; 

        public AttackAction(int damage, float actionCost)
        {
            Damage = damage; 
            ActionCost = actionCost;
        }
    }

    public class MovementEventArgs : EventArgs
    {
        public Cell OriginCell;
        public Cell DestinationCell;
        public List<Cell> Path; 

        public MovementEventArgs(Cell sourceCell, Cell destinationCell, List<Cell> path)
        {
            OriginCell = sourceCell;
            DestinationCell = destinationCell;
            Path = path; 
        }
    }

    public class AttackEventArgs : EventArgs
    {
        public Unit Attacker; 
        public Unit Defender; 

        public int Damage; 

        public AttackEventArgs(Unit attacker, Unit defender, int damage)
        {
            Attacker = attacker; 
            Defender = defender; 
            Damage = damage; 
        }
    }

    public class UnitCreatedEventArgs : EventArgs
    {
        public Transform unit; 
        public UnitCreatedEventArgs(Transform unit)
        {
            this.unit =unit; 
        }
    }
}
