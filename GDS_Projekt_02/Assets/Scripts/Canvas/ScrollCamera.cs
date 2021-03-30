using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    [Header("Pozycja pocz�tkowa kamery")]
    public int startPosCam;
    public int normalPosCam;
    public float sppedChangeCamera;

    [Header("Poruszanie si� kamery")]
    [SerializeField] float movmentSpeed;
    [SerializeField] float minimumYValue;
    [SerializeField] float maximumYValue;

    Camera posCamera;
    float timeToScroll;
    bool startGame = false;

    private void Awake()
    {
        posCamera = GetComponent<Camera>();
        posCamera.orthographicSize = startPosCam;
    }
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (transform.position.y < maximumYValue)
            {
                var newPos = Vector3.up * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (transform.position.y > minimumYValue)
            {
                var newPos = Vector3.down * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }

        }

    }
    public void MoveCameraToNormalPos()
    {
        if (startPosCam != normalPosCam)
        {
            timeToScroll += Time.deltaTime;
           
            posCamera.orthographicSize  = normalPosCam;
        }

    }

}
