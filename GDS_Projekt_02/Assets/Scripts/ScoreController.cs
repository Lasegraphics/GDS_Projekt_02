using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;

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
            }
            if (blueSlider.value != scoreBlueTeam)
            {
                blueText.text = scoreBlueTeam.ToString();
                blueSlider.value = Mathf.MoveTowards(blueSlider.value, scoreBlueTeam, speed * Time.deltaTime);
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
