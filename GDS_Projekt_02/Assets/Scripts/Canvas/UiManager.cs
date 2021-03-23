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
    [Header("Animations")]
    [SerializeField] Text roundToEnd;
    [SerializeField] Animator orangePanel;
    [SerializeField] Animator bluePanel;
    [SerializeField] Animator endRoundText;

    [Header("Desinger button")]
    [SerializeField] Sprite unit;

    [Header("Static")]
    public int currentPlayer = 0;

    [HideInInspector] public bool isStart = true;
    public bool isDesing;

    private void Start()
    {
        endRoundText.SetBool("Out", true);
        CloseEnemyScorePanel();
        CloseScorePanel();

        orangePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-364, 392);
        bluePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(477, 392);
    }
    public void ChangeTurnUi(int player)
    {
        StartCoroutine(CloseEndText());
        StartCoroutine(StartChangeUi(player));      
    }
    IEnumerator StartChangeUi(int player)
    {
        yield return new WaitForSeconds(1);
        currentPlayer = player;
        if (player == 0)
        {
            orangePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-364, 392);
            bluePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(477, 392);
        }
        else
        {
            orangePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(477, 392);
            bluePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-364, 392);
        }
        
    }
    IEnumerator CloseEndText()
    {
        endRoundText.SetBool("Out", false);
        endRoundText.GetComponent<Text>().text = "KONIEC TURY GRACZA :" + (currentPlayer + 1);
        yield return new WaitForSeconds(3);
        endRoundText.SetBool("Out", true);

    }

    public void ActiveEnemyScorePanel()
    {
        bluePanel.SetBool("Out", false);
    }
    public void CloseEnemyScorePanel()
    {
        bluePanel.SetBool("Out", true);
        bluePanel.SetBool("BlinkArmor", false);
        bluePanel.SetBool("BlinkHp", false);
    }
    public void ActiveScorePanel()
    {
        orangePanel.SetBool("Out", false);
    }
    public void CloseScorePanel()
    {
        orangePanel.SetBool("Out", true);
    }
    public void DesingerButton()
    {       
        foreach (var item in FindObjectsOfType<Unit>())
        {
           var Sprite = item.GetComponent<SpriteRenderer>();
            Sprite.color = item.colorUnit;
            Sprite.sprite = unit;
        }
    }
}
