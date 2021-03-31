using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units;
using GridPack.Cells;
using GridPack.Grid;
using GridPack.Grid.UnitGenerators;
using GridPack.Players;
using UnityEngine.UI;

public class NumberUnit : MonoBehaviour
{
    UiManager uiManager;
    ScorePanelControll scorePanelControll;
    CellGrid cellGrid;
    EnemyScorePanel enemyScorePanel;
    StartGameController startGameController;
    public bool isSelected = false;

    int playerNumber;

    private void Awake()
    {
        cellGrid = FindObjectOfType<CellGrid>();
        enemyScorePanel = FindObjectOfType<EnemyScorePanel>();
        uiManager = FindObjectOfType<UiManager>();
        scorePanelControll = FindObjectOfType<ScorePanelControll>();
        startGameController = FindObjectOfType<StartGameController>();
        playerNumber = GetComponent<Unit>().PlayerNumber;     
    }
   
    private void OnMouseEnter()
    {        
        if (uiManager.isStart == false)
        {
            if (playerNumber != cellGrid.CurrentPlayerNumber)
            {
                uiManager.ActiveEnemyScorePanel();
                enemyScorePanel.UpgradeParameters(gameObject);
              
                enemyScorePanel.UpgadeParameters(gameObject.GetComponent<Unit>());
            }
        }
        
    }
    private void OnMouseOver()
    {
        if (uiManager.isStart == false)
        {
            if (playerNumber != cellGrid.CurrentPlayerNumber)
            {
                enemyScorePanel.ChangeHpSlidder(gameObject.GetComponent<Unit>());
                enemyScorePanel.ChangeArmorSlidder(gameObject.GetComponent<Unit>());
            }
        }
    }
    private void OnMouseExit()
    {
        uiManager.CloseEnemyScorePanel();
        enemyScorePanel.RestEvents();
    }
    private void OnMouseDown()
    {
        if (uiManager.isStart == false)
        {
            if (isSelected == false)
            {
                if (playerNumber == cellGrid.CurrentPlayerNumber)
                {
                    foreach (var item in FindObjectsOfType<NumberUnit>())
                    {
                        item.GetComponent<NumberUnit>().isSelected = false;
                    }
                    isSelected = true;
                    uiManager.ActiveScorePanel();
                    scorePanelControll.TakeUnit(gameObject);
                }
            }
        }
    }
    public void DeselectallUnits()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Unit"))
        {
            item.GetComponent<NumberUnit>().isSelected = false;
        }
    }
}

