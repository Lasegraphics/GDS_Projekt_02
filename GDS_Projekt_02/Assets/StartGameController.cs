using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Grid;

public class StartGameController : MonoBehaviour
{

    [SerializeField] GameObject[] panels;
    public int currentTurn;

    UiManager uiManager;
    CellGrid cellGrid;
    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        cellGrid = FindObjectOfType<CellGrid>();
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
        uiManager.isStart = false;
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
        cellGrid.Initialize();
        cellGrid.StartGame();

    }
}
