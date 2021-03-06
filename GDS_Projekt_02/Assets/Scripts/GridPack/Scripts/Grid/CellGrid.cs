﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using characters;
using GridPack.Cells;
using GridPack.Grid.GridStates;
using GridPack.Grid.UnitGenerators;
using GridPack.Players;
using GridPack.Units;
using Random = System.Random;

namespace GridPack.Grid
{
    //Klasa CellGrid śledzi przebieg rozgrywki, przechowuje informacje o komórkach, jednostkach oraz obiektach graczy. Rozpoczyna rozgrywkę i wykonuje przejścia tur
    //Reaguje na interakcje uzytkownika z jednostkami i komórkami oraz wywołuje zdarzenia związane z postępem w grze.  
    public class CellGrid : MonoBehaviour, ISpellSwitcher
    {
        public AudioManager audioManager;
        //LevelLoading jest wywoływane przed uruchomieniem metody initialize.
        public event EventHandler LevelLoading; 
        //LevelLoadingDone zdarzenie jest wywoływane po zakończeniu metody initialize.  
        public event EventHandler LevelLoadingDone;
        //GameStarted zdarzenie jest wywoływane w momencie wywołania metody StartGame
        public event EventHandler GameStarted; 
        //GameEnded zdarzenie jest wywoływane kiedy w grze pozostaje jeden gracz. 
        public event EventHandler GameEnded; 
        //TurnEnded zdarzenie jest wywoływane w momencie zakończenia tury. 
        public event EventHandler TurnEnded; 
        //Zdarzenie jest wywoływane za kazdym razem kiedy metoda AddUnit jest wywoływana. 

        public event EventHandler<UnitCreatedEventArgs> UnitAdded; 

        //Siatka przekazuje część swoich zachowań do obiektu _cellGridState.
        private CellGridState _cellGridState;

        [SerializeField] UiManager uiManager;
        [SerializeField] private GameObject flage;
        public CellGridState CellGridState
        {
            get
            {
                return _cellGridState;
            }
            set
            {
                if(_cellGridState != null)
                    _cellGridState.OnStateExit();
                _cellGridState = value; 
                _cellGridState.OnStateEnter();
            }
        }

        public int NumberOfPlayers {get; private set;}

        public Player CurrentPlayer
        {
            get
            {
                return Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber));
            }
        }
        public int CurrentPlayerNumber {get; private set;}
        //Transform przechowuje informacje na temat graczy. 

        public Transform PlayersParent; 

        public bool GameFinished {get; private set;}
        public List<Player> Players{get; private set;}
        public List<Cell> Cells {get; private set;}
        public List<Unit> Units {get; private set;} 
        public bool IsSwitched {get; private set;} 
        
        private void Start()
        {
            if(LevelLoading != null)
               LevelLoading.Invoke(this, new EventArgs());

            //Initialize();

            if(LevelLoadingDone != null)
               LevelLoadingDone.Invoke(this, new EventArgs());
            //StartGame();
        }

        public void Initialize()
        {
            audioManager.Play("StartGame");
            GameFinished = false; 
            Players = new List<Player>();
            
            for (int i = 0; i < PlayersParent.childCount; i++)
            {
                var player = PlayersParent.GetChild(i).GetComponent<Player>();
                if(player != null)
                   Players.Add(player);
                else 
                    Debug.LogError("Invalid object in Players Parent game object");
            }
            NumberOfPlayers = Players.Count; 
            CurrentPlayerNumber = Players.Min(p => p.PlayerNumber+1); 

            Cells = new List<Cell>();
            for (int i = 0; i < transform.childCount; i++)
            {
               var cell = transform.GetChild(i).gameObject.GetComponent<Cell>(); 
               if(cell != null)
                Cells.Add(cell);
                else 
                Debug.LogError("Invalid object in cells patern game object");
            }

            foreach (var cell in Cells)
            {
                cell.CellClicked += OnCellClicked; 
                cell.CellHighlighted += OnCellHighlighted;
                cell.CellDehighlighted += OnCellDehighlighted;
                cell.GetComponent<Cell>().GetNeighbours(Cells); 
            }

            Units = new List<Unit>();
            var UnitGenerator = GetComponent<IUnitGenerator>();
            if(UnitGenerator != null)
            {
                var units = UnitGenerator.SpawnUnits(Cells);
                foreach (var unit in units)
                {
                    AddUnit(unit.GetComponent<Transform>());
                }
            }
            else 
            Debug.LogError("No IUnitGenerators script attached to cell grid");   
        }

        private void OnCellDehighlighted(object sender, EventArgs e)
        {
            CellGridState.OnCellDeselected(sender as Cell);
        }
        private void OnCellHighlighted(object sender, EventArgs e)
        {
            CellGridState.OnCellSelected(sender as Cell);
        }
        private void OnCellClicked(object sender, EventArgs e)
        {
            CellGridState.OnCellClicked(sender as Cell);
        }

        private void OnUnitClicked(object sender, EventArgs e)
        {
            CellGridState.OnUnitClicked(sender as Unit);
        }
        private void OnUnitDestroyed(object sender, AttackEventArgs e)
        {
            Units.Remove(sender as Unit);
            var totalPlayersAlive = Units.Select(u => u.PlayerNumber).Distinct().ToList();
            if(totalPlayersAlive.Count == 1)
            {
                CellGridState = new CellGridStateBlockInput(this);
                GameFinished = true; 
                if(GameEnded != null)
                    GameEnded.Invoke(this, new EventArgs());
            }
        }

        public void AddUnit(Transform unit)
        {
            Units.Add(unit.GetComponent<Unit>());
            unit.GetComponent<Unit>().UnitClicked += OnUnitClicked; 
            unit.GetComponent<Unit>().UnitDestroyed += OnUnitDestroyed;

            if(UnitAdded != null)
                UnitAdded.Invoke(this, new UnitCreatedEventArgs(unit));
        }

        public void StartGame()
        {
            if(GameStarted != null)
                GameStarted.Invoke(this,new EventArgs());
            Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u=>{u.OnTurnStart(); });
            Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)).Play(this);
            Debug.Log("Game Started");
        }

        public void Chagne()
        {
            if (uiManager.currentPlayer == 0)
            {
                flage.SetActive(true);
            }
            else
            {
                flage.SetActive(false);
            }
        }
        public void EndTurn()
        {
           
            foreach (var item in Units)
            {
                if (item.TotalArmorPoints >0)
                {
                    item.ArmorPoints = item.TotalArmorPoints;
                }
                item.blockChecker = false;
            }
            //CellGridState = new CellGridState(this);
            CellGridState = new CellGridStateBlockInput(this);
            _cellGridState.OnStateEnter();
            List<Cell> cellTypeRuins = new List<Cell>(); 
            
            if(Units.Select(u => u.PlayerNumber).Distinct().Count() == 1)
            {
                return; 
            }

            Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u=>{u.OnTurnStart(); });
            CurrentPlayerNumber = (CurrentPlayerNumber + 1) % NumberOfPlayers;
            while(Units.FindAll(u =>u.PlayerNumber.Equals(CurrentPlayerNumber)).Count == 0)
            {
                CurrentPlayerNumber = (CurrentPlayerNumber + 1) % NumberOfPlayers;
            }

            if(TurnEnded != null)
                TurnEnded.Invoke(this, new EventArgs());     
            uiManager.ChangeTurnUi(CurrentPlayerNumber);
            uiManager.CloseScorePanel();
            uiManager.CloseEnemyScorePanel();
            Debug.Log(string.Format("Player{0} turn", CurrentPlayerNumber));
            Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u=>{u.OnTurnStart(); });
            Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)).Play(this);
            foreach (var item in FindObjectsOfType<NumberUnit>())
            {
                if (item.GetComponent<Unit>().PlayerNumber != CurrentPlayerNumber)
                {
                    var highlighter = item.transform.Find("WhiteTile").GetComponent<SpriteRenderer>();
                    if (highlighter != null)
                    {
                        highlighter.color = new Color(1, 1, 1, 0);
                    }
                }
            }
            foreach (var cell in Cells)
            {
                if(cell.Ruins == true)
                {                  
                    
                    cellTypeRuins.Add(cell);
                    Debug.Log(cellTypeRuins);
                   // uiManager.UpdateToRuins(cell.gameObject);
                }
                /*if(cell.Temple == true)
                {
                    cell.Temple = false;
                    uiManager.UpdateToRuins(cell.gameObject);
                    cell.Ruins = true; 
                }
                */
                
            }
            if(cellTypeRuins.Count() == 4)
            {
                Random random = new Random();
                var randomizedCell = random.Next(cellTypeRuins.Count);
                Debug.Log(cellTypeRuins[randomizedCell]);
                cellTypeRuins[randomizedCell].Ruins = false;
                cellTypeRuins[randomizedCell].Temple = true;

                if(cellTypeRuins[randomizedCell].Ruins == false)
                {
                   uiManager.UpdateToRuins(cellTypeRuins[randomizedCell].gameObject);
                   Debug.Log("Ruiny");
                }
                if(cellTypeRuins[randomizedCell].Temple == true)
                {
                   uiManager.UpdateToTample(cellTypeRuins[randomizedCell].gameObject);
                   Debug.Log("Temple");
                }
            }

            
            
            
        }

        public void Activate()
        {
            IsSwitched = true; 
        }

        public void Deactivate()
        {
            IsSwitched = false; 
        }
    }
}

