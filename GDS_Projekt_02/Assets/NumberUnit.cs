using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberUnit : MonoBehaviour
{
    UiManager uiManager;
    ScorePanelControll scorePanelControll;
    UnitScrollbar unitScrollbar;
    public bool isSelected = false;
    private void Awake()
    {
        unitScrollbar = FindObjectOfType<UnitScrollbar>();
        uiManager = FindObjectOfType<UiManager>();
        scorePanelControll = FindObjectOfType<ScorePanelControll>();
    }
    private void OnMouseEnter()
    {
        unitScrollbar.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        unitScrollbar.gameObject.SetActive(false);
    }
    private void OnMouseDown()
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
