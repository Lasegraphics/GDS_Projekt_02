using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Grid;
using GridPack.SceneScripts;

public class StartGameController : MonoBehaviour
{

    [SerializeField] GameObject[] panels;
    public GameObject buttonStartGame;
    public int currentTurn;

   public UiManager uiManager;
    public CellGrid cellGrid;
    public TurnChanger turnChanger;
    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        cellGrid = FindObjectOfType<CellGrid>();
        turnChanger = FindObjectOfType<TurnChanger>();
    }
    public void ChangeTurn()
    {
        if (currentTurn==0)
        {
            currentTurn = 1;
        }
        else
        {
            currentTurn = 0;
        }
        
    }
    public void StartMainGame()
    {
        turnChanger.StartGame();
        uiManager.isStart = false;
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
        cellGrid.Initialize();
        cellGrid.StartGame();


    }
    
}
