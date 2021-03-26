using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToMousePosCanvas : MonoBehaviour
{
   public Canvas myCanvas;
    public int changeX;

    void Start()
    {
    }
   public void UpdatePos( )
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos.x+ changeX, pos.y, 0);
    }
}
