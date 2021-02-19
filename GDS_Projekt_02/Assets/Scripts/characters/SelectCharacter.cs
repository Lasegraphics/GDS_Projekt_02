using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{

    public bool isSelected = false;

    void Start()
    {
    }

    // Update is called once per frame
    private void OnMouseDown()
    {

        foreach (var item in FindObjectsOfType<SelectCharacter>())
        {
            item.isSelected = false;

        }
            Wybrany();      
    }
    private void OnMouseOver()
    {
        if (isSelected == false)
        {
            if (Input.GetMouseButtonDown(1))
            {

                foreach (var item in FindObjectsOfType<SelectCharacter>())
                {
                    if (item.isSelected)
                    {
                        Debug.Log(item.name + " atakuje " + gameObject.name);

                    }
                }
            }
        }
       

    }

    private void Wybrany()
    {       

        Debug.Log("Wybrany " + gameObject.name);
        isSelected = true;
    }
}
