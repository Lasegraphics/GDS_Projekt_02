using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    

    [Header("Poruszanie si kamery")]
    [SerializeField] float movmentSpeed;
    [SerializeField] float minimumYValue;
    [SerializeField] float maximumYValue;


    [SerializeField] AudioManager audioManager;
    private void Awake()
    {
        audioManager.Play("MainMusic1");
        StartCoroutine(FirstMusic());
    }
    IEnumerator FirstMusic()
    {
       
        yield return new WaitForSeconds(120);
        audioManager.Play("MainMusic2");
        StartCoroutine(SecondMusic());
    }
    IEnumerator SecondMusic()
    {
        yield return new WaitForSeconds(240);
        audioManager.Play("MainMusic3");
        StartCoroutine(FirstMusic());
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") >0f)
        {
            if (transform.position.y < maximumYValue)
            {
                Debug.Log(1);
                var newPos = Vector3.up * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") <0f)
        {
            if (transform.position.y > minimumYValue)
            {
                var newPos = Vector3.down * movmentSpeed * Time.deltaTime;
                transform.Translate(newPos, Space.World);
            }

        }

    }
   

}

