using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridPack.Units;

public class UnitsButton : MonoBehaviour
{
    public GameObject unitPreFab;
    public int player;

    StartGameController startGameController;

    private void Awake()
    {
        startGameController = FindObjectOfType<StartGameController>();
    }

    private void OnMouseDown()
    {
        if (startGameController.currentPlayer == player)
        {
            var buttons = FindObjectsOfType<UnitsButton>();
            foreach (var button in buttons)
            {
                button.GetComponent<SpriteRenderer>().color = new Color32(65, 65, 65, 255);
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
