using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Grid;
using GridPack.SceneScripts;

public class StartGameController : MonoBehaviour
{
    [Header("Panels to be closed")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] public GameObject buttonStartGame;
    [SerializeField] public int currentPlayer; 

    [Header("Other game soucres")]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private CellGrid cellGrid;
    [SerializeField] private TurnChanger turnChanger;
    [SerializeField] private ScoreController scoreController;

    [HideInInspector] private bool firstRound = true;
    [HideInInspector] public int currentTurn = 0;
    public void ChangeTurn()
    {

        if (firstRound)
        {
            currentPlayer = 1;
            firstRound = false;
            

            panels[0].GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
            panels[1].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            currentTurn++;
            if (currentTurn ==2)
            {
                if (currentPlayer == 0)
                {
                    panels[0].GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
                    panels[1].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                    currentPlayer = 1;
                }
                else
                {
                    panels[0].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                    panels[1].GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
                    currentPlayer = 0;
                }
                currentTurn = 0;
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
