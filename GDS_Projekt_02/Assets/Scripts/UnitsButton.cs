using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units;

public class UnitsButton : MonoBehaviour
{
   public GameObject unitPreFab;
    public int player;
    UnitsButton[] buttons;
    StartGameController startGameController;

    private void Awake()
    {
        startGameController = FindObjectOfType<StartGameController>();
        buttons = FindObjectsOfType<UnitsButton>();
    }
    public void ChangeColorImage()
    {
       
            if (startGameController.currentPlayer == player)
            {
                GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
            }
            else
            {
               GetComponent<SpriteRenderer>().color = new Color32(65, 65, 65, 255);
            }
        
    }
    private void OnMouseDown()
    {
        if (startGameController.currentPlayer == player)
        {
            foreach (var button in buttons)
            {
                if (button.player == startGameController.currentPlayer)
                {
                    button.GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
                }
            }
            GetComponent<SpriteRenderer>().color = Color.white;

            foreach (var item in FindObjectsOfType<SpawnUnits>())
            {
                if (item.player == unitPreFab.GetComponent<Unit>().PlayerNumber)
                {
                    item.SetSelectedUnit(unitPreFab);
                }
            }
        }
       
    }
}
