using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour
{

    public GameObject unit;
    public GameObject parentUnits;
    UnitsButton[] unitsButtons;
    public int player;

    StartGameController startGameController;
    private void Awake()
    {
        startGameController = FindObjectOfType<StartGameController>();
        unitsButtons = FindObjectsOfType<UnitsButton>();
    }
    private void OnMouseDown()
    {
        if (gameObject.tag !="Unit")
        {
            if (startGameController.currentPlayer == player)
            {
                if (unit != null)
                {
                    SpawnUnit();
                    foreach (var item in unitsButtons)
                    {
                        item.ChangeColorImage();
                    }
                    foreach (var item in FindObjectsOfType<SpawnUnits>())
                    {
                        item.unit = null;
                    }
                }
            }
        }             
    }
    public void SetSelectedUnit(GameObject UnitToSlecet)
    {
        unit = UnitToSlecet;
    }
    void SpawnUnit()
    {
        if (player==0)
        {
            var newUnit = Instantiate(unit, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
            foreach (var item in FindObjectsOfType<UnitsButton>())
            {
                if (newUnit.tag == item.tag)
                {
                    item.gameObject.SetActive(false);
                    unit = null;
                }
            }
            if (FindObjectsOfType<NumberUnit>().Length == 10)
            {
                startGameController.buttonStartGame.SetActive(true);
            }
            startGameController.ChangeTurn();
            newUnit.transform.parent = parentUnits.transform;
        }
        else
        {
            var newUnit = Instantiate(unit, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.Euler(0, 180, 0));
            foreach (var item in FindObjectsOfType<UnitsButton>())
            {
                if (newUnit.tag == item.tag)
                {
                    item.gameObject.SetActive(false);
                    unit = null;
                }
            }
            if (FindObjectsOfType<NumberUnit>().Length == 10)
            {
                startGameController.buttonStartGame.SetActive(true);
            }
            startGameController.ChangeTurn();
            newUnit.transform.parent = parentUnits.transform;
        }
       
       
    }
}
