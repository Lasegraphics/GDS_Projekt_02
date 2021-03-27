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
    ScrollCamera scrollCamera;

    FieldParameters fieldParameters;
    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        cellGrid = FindObjectOfType<CellGrid>();
        turnChanger = FindObjectOfType<TurnChanger>();
        scoreController = FindObjectOfType<ScoreController>();
        scrollCamera = FindObjectOfType<ScrollCamera>();
        fieldParameters = FindObjectOfType<FieldParameters>();
    }
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
            tourCurrent++;
            if (tourCurrent ==2)
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
        scrollCamera.MoveCameraToNormalPos();
     //   fieldParameters.StartGame();
    }

}
