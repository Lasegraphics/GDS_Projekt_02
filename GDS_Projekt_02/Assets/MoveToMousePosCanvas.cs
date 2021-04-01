using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveToMousePosCanvas : MonoBehaviour
{
   public Canvas myCanvas;
    public int changeX;
    public TextMeshProUGUI text;
    public int dodge = 0;

   public void UpdatePos(int area )
    {
        switch (area)
        {
            case 0:
                text.text = "IMPASSABLE";
                break;
            case 1:
                text.text = "ANY UNIT THAT CROSSES THIS TILE OR STARTS ITS TURN ON IT TAKES 20 DAMAGE";
                break;
            case 2:
                text.text = ("20% CHANCE TO DODGE INCOMING ATTACK.");
                break;
            case 3:
                text.text = "ANY UNIT THAT CROSSES THIS TILE IS FORCED TO END ITS TURN.";
                break;
            case 4:
                text.text = "RESTORES 20 HP AT THE END OF THE TURN. BECOMES RUINS AFTER";
                break;
            case 5:
                text.text = "HAS A CHANCE TO BECOME A TEMPLE AFTER USAGE OF THE EXISTING ONE";
                break;
            case 6:
                text.text = "NO EFFECT";
                break;
            default:
                break;
        }


        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos.x+ changeX, pos.y, 0);
    }
}
