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
    public int currentPlayer;
    bool firstRound = true;
    int tourCurrent = 0;

    UiManager uiManager;
    CellGrid cellGrid;
    TurnChanger turnChanger;
    ScoreController scoreController;
    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        cellGrid = FindObjectOfType<CellGrid>();
        turnChanger = FindObjectOfType<TurnChanger>();
        scoreController = FindObjectOfType<ScoreController>();
    }
    public void ChangeTurn()
    {

        if (firstRound)
        {
            currentPlayer = 1;
            firstRound = false;
        }
        else
        {
            tourCurrent++;
            if (tourCurrent ==2)
            {
                if (currentPlayer == 0)
                {
                    currentPlayer = 1;
                }
                else
                {
                    currentPlayer = 0;
                }
                tourCurrent = 0;
            }
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
        scoreController.StartGame();
    }

}
