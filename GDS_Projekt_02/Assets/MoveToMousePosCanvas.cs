using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToMousePosCanvas : MonoBehaviour
{
   public Canvas myCanvas;
    public int changeX;
    public Text text;

   public void UpdatePos(int area )
    {
        switch (area)
        {
            case 0:
                text.text = "X damage at the beginning of next turn";
                break;
            case 1:
                text.text = "X% chance to dodge";
                break;
            case 2:
                text.text = "Heal X at the end of current turn";
                break;
            default:
                break;
        }


        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos.x+ changeX, pos.y, 0);
    }
}
