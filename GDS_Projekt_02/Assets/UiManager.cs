using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using GridPack.Cells;
using GridPack.Grid.GridStates;
using GridPack.Grid.UnitGenerators;
using GridPack.Players;
using GridPack.Units;

public class UiManager : MonoBehaviour
{
    [SerializeField] Button menuButton;
    [SerializeField] Button endRound;
    [SerializeField] Text roundToEnd;
    [SerializeField] Animator ScorePanel;
    [SerializeField] Animator endRoundText;


    private void Start()
    {
        endRoundText.SetBool("Out", true);
        CloseScorePanel();
    }
    public void ActiveEndText(int player)
    {
        endRoundText.SetBool("Out", false);
        endRoundText.GetComponent<Text>().text = "Koniec rundy gracza :" + player;
        StartCoroutine(CloseEndText());
    }
    IEnumerator CloseEndText()
    {
        yield return new WaitForSeconds(3);
        endRoundText.SetBool("Out", true);

    }
    public void ActiveScorePanel()
    {
        ScorePanel.SetBool("Out", false);
    }
    public void CloseScorePanel()
    {
        ScorePanel.SetBool("Out", true);
    }
    public void EndButton()
    {

    }
    public void OpenMenu()
    {

    }

}
