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
    UnitScrollbar unitScrollbar;
    CellGrid cellGrid;
    public bool isSelected = false;
    int playerNumber;
    private void Awake()
    {
        cellGrid = FindObjectOfType<CellGrid>();

        unitScrollbar = FindObjectOfType<UnitScrollbar>();
        uiManager = FindObjectOfType<UiManager>();
        scorePanelControll = FindObjectOfType<ScorePanelControll>();
        playerNumber = GetComponent<Unit>().PlayerNumber;
    }
    private void OnMouseEnter()
    {
        if (playerNumber != cellGrid.CurrentPlayerNumber)
        {
            unitScrollbar.gameObject.SetActive(true);
            unitScrollbar.UpgradeTextBar(gameObject.GetComponent<Unit>().TotalHitPoints, gameObject.GetComponent<Unit>().HitPoints);
        }
    }
    private void OnMouseExit()
    {
        unitScrollbar.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (playerNumber == cellGrid.CurrentPlayerNumber)
        {
            uiManager.ActiveScorePanel();
            foreach (var item in GameObject.FindGameObjectsWithTag("Unit"))
            {
                item.GetComponent<NumberUnit>().isSelected = false;
            }
            isSelected = true;
            scorePanelControll.TakeUnit(gameObject);
        }

    }
}

