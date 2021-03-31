﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using GridPack.Cells;
using GridPack.Pathfinding.Algorithms;
using GridPack.Units.UnitStates;
using GridPack.Grid; 
using GridPack.Grid.GridStates; 
using GridPack.SceneScripts;
using Random = System.Random;


namespace GridPack.Units
{
    //Bazowa Klasa reprezentująca jednostkę
   [ExecuteInEditMode]
    public abstract class Unit : MonoBehaviour
    {
        Dictionary<Cell, List<Cell>> catchedPaths = null;  

        //UnitClicked jest wywoływane w momencie naciśnięcia na jednostkę. 
        public event EventHandler UnitClicked; 
        //UnitSelected jest wywoływane w momencie kiedy gracz nacisnął na jednostkę nalezącą do niego. 
        public event EventHandler UnitSelected; 
        //UnitDeselected jest wywoływane w momencie kiedy gracz nacisnął na pole inne niz te nalezące do jednostki 
        public event EventHandler UnitDeselected;
        //UnitHighlighted jest wywoływane w momencie kiedy kursor najezdza na jednostkę 
        public event EventHandler UnitHighlighted; 
        //UnitDehighlighted jest wywołwywane w momencie kiedy kursor opuszcza obszar jednostki. 
        public event EventHandler UnitDehighlighted;
        //UnitAttacked jest wywoływane w momencie kiedy jednostka atakuje. 
        public virtual event EventHandler<AttackEventArgs> UnitAttacked;
        //UnitDestroyed jest wywoływane w momencie kiedy jednostka jest niszczona 
        public virtual event EventHandler<AttackEventArgs> UnitDestroyed;   
        //UnitMoved jest wywoływane w momencie kiedy jednostka sie przemieszcza.
        public event EventHandler<MovementEventArgs> UnitMoved;   


        public UnitState UnitState {get; set;} 
        public void SetState(UnitState state)
        {
            UnitState.MakeTransition(state);
        }

        //Lista Buffów które są zaaplikowane do jednostki 
        public List<Buff> Buffs {get; set;}

        public int TotalHitPoints {get; private set;}
        public float TotalMovementPoints{get; private set;}
        public float TotalActionPoints {get; private set;}

  

        //Reprezentacja pola zajmowanego przez jednostkę 
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
        [Header("DO EDYCJI")]
        public int HitPoints; 
        public int AttackRange;
        public int AttackFactor;
        public int ArmorPoints;
        public bool ignorArmor;
        public Color colorUnit;
        public float actionPoints = 1; //Determinuje Jak duzo ataków moze wykonać jednostka. 
        public  int RandomHitPercentUnit;
        public  int HitSpikeParameterUnit;
        public  int HealTempleParameterUnit;
        [SerializeField] private float movementPoints; //Determinuje jak daleko po siatce jednostka moze sie przemieszczac. 


        [Header("nie potrzebne")]

        //Determinuje szybkość przemieszczania jednostki. 
        public string nameUnit;
        public float MovementAnimationSpeed;
        public int PlayerNumber;
        public Sprite StartSprite;

        [HideInInspector] public int TotalArmorPoints;
        [HideInInspector] public bool isBlinking;
        UiManager uiManager;
        ScoreController scoreController;
        ScorePanelControll scorePanelControll;
        AudioManager audioManager;

        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

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

        //Wskazuje gracza do którego nalezy jednostka
        //Powinien korespondować ze zmienna PlayerNumber w skrypcie gracza.  
        public CellGrid EndTrn {get; set;}

        //Wskazuje jesli animacja ruchu jest odpalona. 
        public bool IsMoving{get; set;}
    
        //Implementacja algorytmów
        private static DijkstraPathfinding _pathfinder = new DijkstraPathfinding(); 
        private static IPathfinding _fallbackPathfinder = new AStarPathfinding();

        //Metoda wywoływana w momencie utworzenia obiektu w celu zainicjowania pól. 
        public virtual void Initialize()
        {
            Buffs = new List<Buff>();
            UnitState = new UnitStateNormal(this);
            EndTrn = new CellGrid();
            TotalHitPoints = HitPoints; 
            TotalMovementPoints = MovementPoints;
            TotalActionPoints = ActionPoints; 
           // Debug.Log("obecne zdrowie: " + HitPoints);

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

        //Metoda jest wywoływana na początku kazdej tury. 
         public virtual void OnTurnStart()
        {
            MovementPoints = TotalMovementPoints;
            ActionPoints = TotalActionPoints;

            if(Cell != null && Cell.Spikes == true)
            {
                Debug.Log("Zadano Obrazenia");
                HitPoints -= HitSpikeParameterUnit;
            }

            if(Cell != null && Cell.Temple == true)
            {
                Debug.Log("Uzdrowiono");
                HitPoints += HealTempleParameterUnit;
                Cell.Temple = false; 
            }


            SetState(new UnitStateMarkedAsFriendly(this)); 
        }

        //Metoda jest wywoływana na końcu kazdej tury. 
        public virtual void OnTurnEnd()
        {
            catchedPaths = null; 
            Buffs.FindAll(b =>  b.Duration == 0).ForEach(b => {b.Undo(this);});
            Buffs.RemoveAll(b => b.Duration ==0);
            Buffs.ForEach(b => { b.Duration--; });

            SetState(new UnitStateNormal(this)); 
        }

        //Metoda jest wywoływana kiedy spadnie HP ponizej 1
        protected virtual void OnDestroyed()
        {
            Cell.IsBlocked = false; 
            MarkAsDestroyed();
            gameObject.SetActive(false);
        }

        //Metoda jest wywoływana w momencie zaznaczenia jednostki
        public virtual void OnUnitSelected()
        {
            uiManager = FindObjectOfType<UiManager>();
            scorePanelControll = FindObjectOfType<ScorePanelControll>();
            SetState(new UnitStateMarkedAsSelected(this));
            if(UnitSelected != null)
            {
                UnitSelected.Invoke(this, new EventArgs());
            }
           
            if (Cell.Forest || Cell.Spikes||Cell.Temple )
            {
                scorePanelControll.UpgadeParameters(this);
            }
            else
            {
                scorePanelControll.RestEvents();

            }
            uiManager.ActiveScorePanel();
        }

        //Metoda jest wywoływana w momencie odznaczenia jednostki
        public virtual void OnUnitDeselected()
        {
            uiManager = FindObjectOfType<UiManager>();
            SetState(new UnitStateMarkedAsFriendly(this));
           
            if (UnitDeselected != null)
            {            
                UnitDeselected.Invoke(this, new EventArgs());
            }
            uiManager.CloseScorePanel();
            

        }

        //Metoda wskazuje czy mozna zaatakowac jednostkę z danej komórki 
        public virtual bool IsUnitAttackable(Unit other, Cell sourceCell)
        {
            if(AttackRange == 1)
            {
                return sourceCell.GetDistance(other.Cell) == AttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1; 
            }
            else
            {
                return sourceCell.GetDistance(other.Cell) < AttackRange
                && other.PlayerNumber != PlayerNumber
                && ActionPoints >= 1; 
            }
           
        }

        public virtual bool UnitIsntAttackable(Unit other, Cell sourceCell)
        {
           return sourceCell.GetDistance(other.Cell) >= AttackRange
            && other.PlayerNumber != PlayerNumber
            && ActionPoints <= 1; 
        }

        //Metoda wykonuje atak na daną jednostkę
        public void AttackHandler(Unit unitToAttack)
        {
            if (PlayerNumber == 1)
            {
                if (unitToAttack.gameObject.transform.position.x > gameObject.transform.position.x)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                    MainAttack(unitToAttack);
                    StartCoroutine(SwapUnit());
                }
                else
                {
                    MainAttack(unitToAttack);
                }
            }
            else
            {
                if (unitToAttack.gameObject.transform.position.x < gameObject.transform.position.x)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
                    MainAttack(unitToAttack);
                    StartCoroutine(SwapUnit());
                }
                else
                {
                    MainAttack(unitToAttack);
                }
            }        
        }
        IEnumerator SwapUnit()
        {
            yield return new WaitForSeconds(0.5f);
            if (PlayerNumber ==0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void MainAttack(Unit unitToAttack)
        {
            if (!IsUnitAttackable(unitToAttack, Cell))
            {
                return;
            }
            if (gameObject.GetComponent<Wizard>() != null)
            {
                audioManager.Play("MagicAttack");
                Debug.Log(1);
            }
            if (AttackRange<=1)
            {
                audioManager.Play("MeleAtack");
            }
            if(AttackRange>1 && gameObject.GetComponent<Wizard>() == null)
            {
                audioManager.Play("BowAtack");
                
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

        //Matoda oblicza koszt punktów ataku i obrazeń podczas walki  
        protected virtual void AttackActionPerformed(float actionCost)
        {
            ActionPoints -= actionCost; 
            if(ActionPoints == 0)
            {
                MovementPoints = 0; 
                SetState(new UnitStateMarkedAsFinished(this));
                // EndTrn.EndTurn();
               
            }
        }

        //Metoda obsługi obrony przed atakiem. Do rozkminienia 
        public virtual void DefendHandler(Unit aggressor, int damage)
        {
            
           Random rand = new Random();
            int randInt = rand.Next(0,100);
            if(Cell != null && Cell.Forest == true)
            {

                if (ArmorPoints > 0 && aggressor.GetComponent<Wizard>() == null)
                {
                    MarkAsDefending(aggressor);
                    int damageTaken = aggressor.AttackFactor;
                    if(randInt <= RandomHitPercentUnit)
                    {
                        ArmorPoints -= damageTaken;
                        DefenceActionPerformed();
                        Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
                    }
                    else 
                    {
                        Debug.Log("Defence");
                    }
                    
                    
                }
                else
                {
                    if (ArmorPoints <= 0 || aggressor.ignorArmor == true)
                    {
                        MarkAsDefending(aggressor);
                        int damageTaken = aggressor.AttackFactor;
                        if(randInt <= RandomHitPercentUnit)
                        {
                            HitPoints -= damageTaken;
                            DefenceActionPerformed();
                            Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
                        }
                        else 
                        {
                            Debug.Log("Defence");
                        }
                        
                        if (HitPoints <= 0)
                        {
                            if (UnitDestroyed != null)
                            {
                                UnitDestroyed.Invoke(this, new AttackEventArgs(aggressor, this, damage));
                            }
                            OnDestroyed();
                        }
                        
                    }
                }
            }
            if(Cell != null && Cell.Forest == false)
            {
                if (ArmorPoints > 0 && aggressor.GetComponent<Wizard>() == null)
                {
                    MarkAsDefending(aggressor);
                    int damageTaken = aggressor.AttackFactor;
                    ArmorPoints -= damageTaken;
                    DefenceActionPerformed();
                    Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
                    
                }
                else
                {
                    if (ArmorPoints <= 0 || aggressor.ignorArmor == true)
                    {
                        MarkAsDefending(aggressor);
                        int damageTaken = aggressor.AttackFactor;
                        HitPoints -= damageTaken;
                        DefenceActionPerformed();
                        if (HitPoints <= 0)
                        {
                            if (UnitDestroyed != null)
                            {
                                UnitDestroyed.Invoke(this, new AttackEventArgs(aggressor, this, damage));
                            }
                            OnDestroyed();
                        }
                        Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
                    }
                }
            }
            if (UnitAttacked != null)
            {
                UnitAttacked.Invoke(this, new AttackEventArgs(aggressor, this, damage));
            }
            scoreController = FindObjectOfType<ScoreController>();
            scoreController.UpgradeScore();
        }
        

       

        protected virtual void DefenceActionPerformed(){}

        //Metoda obsługi poruszania jednostki. 
        public virtual void Move(Cell destinationCell, List<Cell> path)
        {
            var totalMovementCost = path.Sum(h => h.MovementCost);
            scorePanelControll = FindObjectOfType<ScorePanelControll>();
            MovementPoints -= totalMovementCost;
            scorePanelControll.UpgradeMovment(this);
            Cell.IsBlocked = false;
            Cell.CurrentUnit = null;
            Cell = destinationCell; 
            destinationCell.IsBlocked = true; 
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

            if(destinationCell.Spikes == true)
            {
                Debug.Log("Zadano Obrazenia");
                HitPoints -= HitSpikeParameterUnit;
            }

            if(destinationCell.Swamp == true)
            {
                Debug.Log("Bagno");
                MovementPoints = 0; 
            }
        
        }

        //Metoda obsługuje animacje poruszania jednostki. 
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
            if (PlayerNumber == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            OnMoveFinished();
            
        }

        //Metoda wywoływana po zakończeniu animacji
        protected virtual void OnMoveFinished(){}

        //Metoda wskazuje czy jednostka moze przejść do komórki podanej jako parametr 
        public virtual bool IsCellMovableTo(Cell cell)
        {
            return !cell.IsBlocked; 
        }

        //Metoda wskazuje czy jednostka moze przejść przez komórki podane jako parametr 
        public virtual bool IsCellTraversable(Cell cell)
        {
            return !cell.IsBlocked; 
        }

        //Metoda zwraca wszystkie komórki do których jednostka moze się udać 
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

        public List<Cell> FindPath(List<Cell> cells, Cell destination)
        {
            if (destination.gameObject.transform.position.x >= transform.position.x)
            {
                if (PlayerNumber ==0) 
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);                
                }
                else 
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

            }
            else
            {
                if (PlayerNumber == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);

                }
                else //prawy gracz
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

            }
            if (catchedPaths != null && catchedPaths.ContainsKey(destination))
            {
                return catchedPaths[destination];
            }
            else
            {
                return _fallbackPathfinder.FindPath(GetGraphEdges(cells), Cell, destination);
            }
        }

        //Metoda zwraca grafową reprezentacje komórki dla wyznaczenia trasy. 
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
                Cell.IsBlocked = false; 
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
