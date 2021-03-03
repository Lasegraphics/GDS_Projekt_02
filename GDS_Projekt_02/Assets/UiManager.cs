using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Button menuButton;
    [SerializeField] Button endRound;
    [SerializeField] Text roundToEnd;

    public void EndButton()
    {

    }
    public void OpenMenu()
    {

    }
    public void ChangeTextRound(int number)
    {
        int actualRound = number;
        roundToEnd.text = actualRound.ToString();
    }
}
