using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
using GridPack.SceneScripts;

public class ScoreController : MonoBehaviour
{
    [Header("Blue Team")]
    public Slider blueSlider;
    public Text blueText;
    public int scoreBlueTeam;
    
    [Header("Red Team")]
    public Slider redSlider;
    public Text redText;
    public int scoreRedTeam;

    [Header("Parameters")]
    public int speed;
    UiManager uiManager;

    [Header("Victory Screens")]
    public GameObject blueWin;
    public GameObject orangeWin;
    public Canvas[] canvasToOff;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
    }
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
                    blueWin.SetActive(true);
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
                    orangeWin.SetActive(true);
                    Destroy(FindObjectOfType<ScrollCamera>());
                    foreach (var item in canvasToOff)
                    {
                        item.gameObject.SetActive(false);
                    }
                    foreach (var item in FindObjectsOfType<NumberUnit>())
                    {
                       
                        Debug.Log(1);
                        Destroy(item.GetComponent<Unit>());
                       
                    }
                }
            }
        }
       
         
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
