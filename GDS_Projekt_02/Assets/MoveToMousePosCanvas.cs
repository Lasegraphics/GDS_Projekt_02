using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveToMousePosCanvas : MonoBehaviour
{
   public Canvas myCanvas;
    public int changeX;
    public TEXT text;
    public int dodge = 0;

   public void UpdatePos(int area )
    {
        switch (area)
        {
            case 0:
                text.text = "Units can’t move onto tile";
                break;
            case 1:
                text.text = "Any unit that crosses this tile or starts its turn on it takes X damage";
                break;
            case 2:
                text.text = (dodge+"% chance to dodge incoming attack");
                break;
            case 3:
                text.text = "Any unit that crosses this tile will be forced to end its turn";
                break;
            case 4:
                text.text = "Restores X HP at the end of the turn. Becomes Ruins after";
                break;
            case 5:
                text.text = "Has a chance to become a Temple after usage of the current one";
                break;
            case 6:
                text.text = "No effect";
                break;
            default:
                break;
        }


        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos.x+ changeX, pos.y, 0);
    }
}
