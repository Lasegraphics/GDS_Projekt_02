using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
using GridPack.SceneScripts;

public class ScoreController : MonoBehaviour
{
    [Header("Blue Team")]
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Text blueText;
    [SerializeField] private int scoreBlueTeam;
    
    [Header("Red Team")]
    [SerializeField] private Slider redSlider;
    [SerializeField] private Text redText;
    [SerializeField] private int scoreRedTeam;

    [Header("Parameters")]
    [SerializeField] private int speed;
    [SerializeField] private UiManager uiManager;

    [Header("Victory Screens")]
    [SerializeField] private Animator blueWin;
    [SerializeField] private Animator orangeWin;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;
    [SerializeField] private Canvas[] canvasToOff;

   
    public void StartGame()
    {
        foreach (var item in FindObjectsOfType<Unit>())
        {
            if (item.PlayerNumber ==0)
            {
                scoreBlueTeam += item.TotalHitPoints;
                blueSlider.maxValue = scoreBlueTeam;
                blueSlider.value = scoreBlueTeam;
                blueText.text = scoreBlueTeam.ToString();

            }
            else
            {
                scoreRedTeam+= item.TotalHitPoints;
                redSlider.maxValue = scoreRedTeam;
                redSlider.value = scoreRedTeam;
                redText.text = scoreRedTeam.ToString();

            }
        }
    }

    void Update()
    {
        if (uiManager.isStart==false)
        {
            if (redSlider.value != scoreRedTeam)
            {
                redSlider.value = Mathf.MoveTowards(redSlider.value, scoreRedTeam, speed * Time.deltaTime);
                redText.text = scoreRedTeam.ToString();
                if (redSlider.value==0)
                {
                    StartCoroutine(Start1Buttons());
                    StartCoroutine(Star2Buttons());
                    StartCoroutine(Star3Buttons());
                    blueWin.SetBool("In", true);
                    Destroy(FindObjectOfType<ScrollCamera>());
                    foreach (var item in canvasToOff)
                    {
                        item.gameObject.SetActive(false);
                    }
                    foreach (var item in FindObjectsOfType<Unit>())
                    {

                        Destroy(item.GetComponent<NumberUnit>());
                        Destroy(item);

                    }
                    foreach (var item in FindObjectsOfType<MyOtherHexagon>())
                    {
                        Destroy(item);
                    }
                }
            }
            if (blueSlider.value != scoreBlueTeam)
            {
                blueText.text = scoreBlueTeam.ToString();
                blueSlider.value = Mathf.MoveTowards(blueSlider.value, scoreBlueTeam, speed * Time.deltaTime);
                if (blueSlider.value==0)
                {
                    orangeWin.SetBool("In", true);
                    Destroy(FindObjectOfType<ScrollCamera>());
                    foreach (var item in canvasToOff)
                    {
                        item.gameObject.SetActive(false);
                    }
                    foreach (var item in FindObjectsOfType<NumberUnit>())
                    {
                       
                        Destroy(item.GetComponent<Unit>());
                       
                    }
                }
            }
        }
       
         
    }
    IEnumerator Start1Buttons()
    {
        yield return new WaitForSeconds(0.75f);
        button1.SetActive(true);
    }
    IEnumerator Star2Buttons()
    {
        yield return new WaitForSeconds(1.5f);
        button2.SetActive(true);
    }
    IEnumerator Star3Buttons()
    {
        yield return new WaitForSeconds(2.25f);
        button3.SetActive(true);
    }
    public void UpgradeScore()
    {
        scoreRedTeam = 0;
        scoreBlueTeam = 0;
        foreach (var item in FindObjectsOfType<Unit>())
        {
            if (item.PlayerNumber == 0)
            {
                scoreBlueTeam += item.HitPoints;
            }
            else
            {
                scoreRedTeam += item.HitPoints;                
            }
        }

    }
}
