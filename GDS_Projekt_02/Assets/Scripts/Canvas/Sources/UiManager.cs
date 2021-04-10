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
    [SerializeField] private Text roundToEnd;
    [SerializeField] private Animator orangePanel;
    [SerializeField] private Animator bluePanel;
    [SerializeField] private Animator endRoundText;
    [SerializeField] private AudioManager audioManager;

    [Header("Desinger button")]
    [SerializeField] private Sprite unit;
    [SerializeField] private Sprite groundNormal;
    [SerializeField] private Sprite groundForest;
    [SerializeField] private Sprite groundMountains;
    [SerializeField] private Sprite groundWater;
    [SerializeField] private Sprite groundTemple;
    [SerializeField] private Sprite groundRuins;
    [SerializeField] private Sprite groundLava;

    //UIowe elementy od Wojtka
    [SerializeField] private GameObject FlagOrange;
    [SerializeField] private GameObject FlagBlue;
    [SerializeField] private GameObject ButtonBlocker;

    [Header("Desinger button - Add")]
    [SerializeField] private Sprite templeNormal;
    [SerializeField] private Sprite ruinsNormal;
    [SerializeField] private ScorePanelControll playerPanel;
    [SerializeField] private EnemyScorePanel enemyPane;
    [Header("Static")]
    [SerializeField] public int currentPlayer = 0;
    [SerializeField] public bool isDesing;

    [HideInInspector] public bool isStart = true;
    
    
    private void Start()
    {
        endRoundText.SetBool("Out", true);
        CloseEnemyScorePanel();
        CloseScorePanel();   
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
            FlagBlue.SetActive(true);
            FlagOrange.SetActive(false);
            ButtonBlocker.SetActive(true);
            roundToEnd.text = "BLUE PLAYER'S TURN";
            var oldBluePanelPos = bluePanel.GetComponent<RectTransform>().anchoredPosition;
            var oldOrangePanel = orangePanel.GetComponent<RectTransform>().anchoredPosition;

            bluePanel.GetComponent<RectTransform>().anchoredPosition = oldOrangePanel;
            orangePanel.GetComponent<RectTransform>().anchoredPosition = oldBluePanelPos;
        }
        else
        {
            FlagOrange.SetActive(true);
            FlagBlue.SetActive(false);
            ButtonBlocker.SetActive(true);
            roundToEnd.text = "ORANGE PLAYER'S TURN";
            var oldBluePanelPos = bluePanel.GetComponent<RectTransform>().anchoredPosition;
            var oldOrangePanel = orangePanel.GetComponent<RectTransform>().anchoredPosition;

            bluePanel.GetComponent<RectTransform>().anchoredPosition = oldOrangePanel;
            orangePanel.GetComponent<RectTransform>().anchoredPosition = oldBluePanelPos;
        }
        endRoundText.SetBool("Out", false);
        audioManager.Play("TurnEnd");
        StartCoroutine(CloseEndText());
    }
    IEnumerator CloseEndText()
    {
       yield return new WaitForSeconds(1.5f);
       endRoundText.SetBool("Out", true);
        ButtonBlocker.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        FlagBlue.SetActive(false);
        FlagOrange.SetActive(false);

    }
    public void ActiveEnemyScorePanel()
    {
        enemyPane.hide.SetActive(false);
        bluePanel.SetBool("Out", false);
    }
    public void CloseEnemyScorePanel()
    {
        enemyPane.hide.SetActive(true);
        bluePanel.SetBool("BlinkArmor", false);
        bluePanel.SetBool("BlinkHp", false);
        bluePanel.SetBool("Out", true);       
    }
    public void ActiveScorePanel()
    {
        playerPanel.hide.SetActive(false);
        orangePanel.SetBool("Out", false);
    }
    public void CloseScorePanel()
    {
        playerPanel.hide.SetActive(true);
        orangePanel.SetBool("Out", true);
    }
    public void UpdateToTample(GameObject item)
    {
        item.GetComponent<Cell>().startSprite = templeNormal;
        item.transform.tag = "G-Temple";
        if (!isDesing)
        {
            var mainSprite = item.GetComponent<SpriteRenderer>();
            mainSprite.sprite = templeNormal;
            var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
            childImage.sprite = templeNormal;
        }
        else
        {               
            var mainSprite = item.GetComponent<SpriteRenderer>();
            mainSprite.sprite = groundTemple;
            var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
            childImage.sprite = groundTemple;
        }
    }
    public void UpdateToRuins(GameObject item)
    {
        item.GetComponent<Cell>().startSprite = ruinsNormal;
        item.transform.tag = "G-ruin";
        if (!isDesing)
        {
            var mainSprite = item.GetComponent<SpriteRenderer>();
            mainSprite.sprite = ruinsNormal;
            var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
            childImage.sprite = ruinsNormal;
        }
        else
        {

            var mainSprite = item.GetComponent<SpriteRenderer>();
            mainSprite.sprite = groundRuins;
            var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
            childImage.sprite = groundRuins;
        }
    }
    public void DesingerButton()
    {
        if (!isDesing)
        {
            foreach (var item in FindObjectsOfType<Unit>())   
            {
                item.GetComponent<Animator>().enabled = false;
                var Sprite = item.GetComponent<SpriteRenderer>();
                Sprite.sprite = item.colorUnit;
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
                if (item.transform.Find("Highlighter")!=null)
                {
                    var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                    childImage.sprite = groundMountains;
                }
               
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
                item.GetComponent<Animator>().enabled = true;
                var Sprite = item.GetComponent<SpriteRenderer>();
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
                if (item.transform.Find("Highlighter") !=null)
                {
                    var childImage = item.transform.Find("Highlighter").GetComponent<SpriteRenderer>();
                    childImage.sprite = item.GetComponent<Cell>().startSprite;
                }
                
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
