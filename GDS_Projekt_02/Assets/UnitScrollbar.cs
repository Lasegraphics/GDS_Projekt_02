using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScrollbar : MonoBehaviour
{
    public bool work;
    Text hpUnit;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = new Vector2 (Input.mousePosition.x,Input.mousePosition.y+40);
    }
   public void UptadeTextBar()
    {

    }

}
