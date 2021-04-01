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
    [SerializeField] Sprite groundNormal;
    [SerializeField] Sprite groundForest;
    [SerializeField] Sprite groundMountains;
    [SerializeField] Sprite groundWater;
    [SerializeField] Sprite groundTemple;
    [SerializeField] Sprite groundRuins;
    [SerializeField] Sprite groundLava;

    [Header("Static")]
    public int currentPlayer = 0;

    [HideInInspector] public bool isStart = true;
    public bool isDesing;

    private void Start()
    {
        endRoundText.SetBool("Out", true);
        CloseEnemyScorePanel();
        CloseScorePanel();
       // var oldBluePanelPos = bluePanel.GetComponent<RectTransform>().anchoredPosition;
        //var oldOrangePanel = orangePanel.GetComponent<RectTransform>().anchoredPosition;

       // orangePanel.GetComponent<RectTransform>().anchoredPosition = oldBluePanelPos;
        //bluePanel.GetComponent<RectTransform>().anchoredPosition = oldOrangePanel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DesingerButton();
        }
    }
    public void ChangeTurnUi(int player)
    {
        currentPlayer = player;
        if (player == 0)
        {
            var oldBluePanelPos = bluePanel.GetComponent<RectTransform>().anchoredPosition;
            var oldOrangePanel = orangePanel.GetComponent<RectTransform>().anchoredPosition;

            bluePanel.GetComponent<RectTransform>().anchoredPosition = oldOrangePanel;
            orangePanel.GetComponent<RectTransform>().anchoredPosition = oldBluePanelPos;
        }
        else
        {
            var oldBluePanelPos = bluePanel.GetComponent<RectTransform>().anchoredPosition;
            var oldOrangePanel = orangePanel.GetComponent<RectTransform>().anchoredPosition;

            bluePanel.GetComponent<RectTransform>().anchoredPosition = oldOrangePanel;
            orangePanel.GetComponent<RectTransform>().anchoredPosition = oldBluePanelPos;
        }
        endRoundText.SetBool("Out", false);
        endRoundText.GetComponent<Text>().text = "KONIEC TURY GRACZA :" + player;
        StartCoroutine(CloseEndText());
    }
    IEnumerator CloseEndText()
    {
        yield return new WaitForSeconds(3);
        endRoundText.SetBool("Out", true);

    }

    public void ActiveEnemyScorePanel()
    {
        bluePanel.SetBool("Out", false);
    }
    public void CloseEnemyScorePanel()
    {
        bluePanel.SetBool("BlinkArmor", false);
        bluePanel.SetBool("BlinkHp", false);
        bluePanel.SetBool("Out", true);       
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
        if (isDesing ==false)
        {
            foreach (var item in FindObjectsOfType<Unit>())   
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.color = item.colorUnit;
                Sprite.sprite = unit;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-normal"))  
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundNormal;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundNormal;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Forest"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundForest;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundForest;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Mountain"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundMountains;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundMountains;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Water"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundWater;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundWater;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Temple"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundTemple;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundTemple;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-ruin"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundRuins;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundRuins;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G1-Lava"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = groundLava;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = groundLava;
            }
            isDesing = true;
        }
        else
        {
            foreach (var item in FindObjectsOfType<Unit>())
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.color = new Color(1, 1, 1);
                Sprite.sprite = item.StartSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-normal")) { 
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Forest")) 
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Mountain"))  
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Water")) 
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-Temple"))  
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G-ruin"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("G1-Lava"))
            {
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.GetComponent<Cell>().startSprite;
                var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                childImage.sprite = item.GetComponent<Cell>().startSprite;
            }
            isDesing = false;
        }
        
    }
}
