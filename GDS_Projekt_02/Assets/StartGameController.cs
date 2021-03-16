using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Grid;

public class StartGameController : MonoBehaviour
{

    [SerializeField] GameObject[] panels;

    UiManager uiManager;
    CellGrid cellGrid;
    ScrollCamera scrollCamera;
    private void Awake()
    {
        scrollCamera = FindObjectOfType<ScrollCamera>();
        uiManager = FindObjectOfType<UiManager>();
        cellGrid = FindObjectOfType<CellGrid>();
    }
    void Update()
    {

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
