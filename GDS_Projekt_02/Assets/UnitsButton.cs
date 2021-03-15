using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsButton : MonoBehaviour
{
    public NumberUnit unitPreFab;


    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<UnitsButton>();
        foreach (var button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(65, 65, 65, 255);
        }
        GetComponent<SpriteRenderer>().color = Color.white;

        FindObjectOfType<SpawnUnits>().SetSelectedUnit(unitPreFab);
    }
}
