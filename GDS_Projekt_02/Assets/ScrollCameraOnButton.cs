using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using  UnityEngine.UIElements;
using  UnityEngine.UI;

public class ScrollCameraOnButton : MonoBehaviour
{
    [Header("Camera")] 
    [SerializeField] private int topMaxPos;
    [SerializeField] private int downMaxPos;
    [SerializeField] private int speedCamera;
    [SerializeField] private Camera camer;
    private bool upMouseDown;
    private bool downMouseDown;
    public UnityEvent onLongClick;
    void Update()
    {
        if (upMouseDown)
        {
            if (camer.transform.position.y < topMaxPos)
            {
                Debug.Log(0);
                var newPos = Vector3.up * speedCamera * Time.deltaTime;
                camer.transform.Translate(newPos, Space.World);
            }
        }

        if (downMouseDown)
        {
            if (camer.transform.position.y > downMaxPos)
            {
                var newPos = Vector3.down * speedCamera * Time.deltaTime;
                camer.transform.Translate(newPos, Space.World);
            }  
        }
    }
    public void ScrollUpScreen()
    {
        upMouseDown = true;
        StartCoroutine(StopScroll());
    }

    public void ScrollDownScreen()
    {
        downMouseDown = false;
        StartCoroutine(StopScroll());
    }

    public void Reset()
    {
        upMouseDown = false;
        downMouseDown = false;
    }

    IEnumerator StopScroll()
    {
        yield return new WaitForSeconds(.2f);
        Reset();
    }
}
