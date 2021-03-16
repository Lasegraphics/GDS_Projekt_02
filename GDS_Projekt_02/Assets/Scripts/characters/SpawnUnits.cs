using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour
{

    public NumberUnit unit;
    public GameObject parentUnits;
    private void OnMouseDown()
    {
        if (unit != null)
        {
            AttemptToPlaceUnitAt(GetClicked());
        }
    }
    public void SetSelectedUnit(NumberUnit UnitToSlecet)
    {
        unit = UnitToSlecet;
    }
    private void AttemptToPlaceUnitAt(Vector2 gridPos)
    {
        SpawnUnit(gridPos);
    }
    private Vector2 GetClicked()
    {
        Vector2 clickpos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPas = Camera.main.ScreenToWorldPoint(clickpos);
        Vector2 gridPos = SnapToGrid(worldPas);

        return gridPos;
    }
    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }
    void SpawnUnit(Vector2 worldPos)
    {
       var newUnit = Instantiate(unit, worldPos, transform.rotation);
        newUnit.transform.parent = parentUnits.transform;
    }
}
