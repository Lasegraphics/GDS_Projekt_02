using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    
    


    [SerializeField] AudioManager audioManager;
    Camera posCamera;


    private void Awake()
    {
       
        StartCoroutine(FirstMusic());
    }
    IEnumerator FirstMusic()
    {
        audioManager.Play("MainMusic1");
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
   
   

}
