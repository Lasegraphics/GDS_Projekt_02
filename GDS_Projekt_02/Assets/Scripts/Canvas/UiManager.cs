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
    [SerializeField] Text roundToEnd;
    [SerializeField] Animator scorePanel;
    [SerializeField] Animator enemyScorePanel;
    [SerializeField] Animator endRoundText;
    [SerializeField] Texture2D cursorTexture;


    private void Start()
    {
        endRoundText.SetBool("Out", true);
        CloseEnemyScorePanel();        
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
    public void ActiveEnemyScorePanel()
    {
        enemyScorePanel.SetBool("Out", false);
    }
    public void CloseEnemyScorePanel()
    {
        enemyScorePanel.SetBool("Out", true);
    }
    public void ActiveScorePanel()
    {
        scorePanel.SetBool("Out", false);
    }
    public void CloseScorePanel()
    {
        scorePanel.SetBool("Out", true);
    }
}
