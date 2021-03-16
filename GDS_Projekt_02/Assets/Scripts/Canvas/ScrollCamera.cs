using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    [Header("Pozycja kamery")]
    public int startPosCam;
    public int normalPosCam;
    public float sppedChangeCamera;

    [Header("Poruszanie siê kamery")]
    public float dragSpeed = 2;
    public float outerLeft = -10f;
    public float outerRight = 10f;
   
    private Vector3 dragOrigin;
    bool cameraDragging = true;
    Camera posCamera;
    UiManager uiManager;
    float timeToScroll;
    private void Awake()
    {
        posCamera = GetComponent<Camera>();
        posCamera.orthographicSize = startPosCam;
        uiManager = FindObjectOfType<UiManager>();
    }
    void Update()
    {
        if (uiManager.IsStartGame() == false)
        {
            MoveCameraToNormalPos();
        }
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);
        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        if (mousePosition.x > right)
        {
            cameraDragging = true;
        }
        if (cameraDragging)
        {

            if (Input.GetMouseButtonDown(2))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(2)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

            if (move.x > 0f)
            {
                if (this.transform.position.x < outerRight)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else
            {
                if (this.transform.position.x > outerLeft)
                {
                    transform.Translate(move, Space.World);
                }
            }

            Vector3 move2 = new Vector3(0, pos.y * dragSpeed, 0);

            if (move2.y > 0f)
            {
                if (this.transform.position.y < outerRight)
                {
                    transform.Translate(move2, Space.World);
                }
            }
            else
            {
                if (this.transform.position.y > outerLeft)
                {
                    transform.Translate(move2, Space.World);
                }
            }
        }

    }
    public void MoveCameraToNormalPos()
    {
        if (startPosCam != normalPosCam)
        {
            timeToScroll += Time.deltaTime;
            posCamera.orthographicSize = Mathf.SmoothStep(startPosCam, normalPosCam, timeToScroll);
        }
       
    }
}
