using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{

    public bool isSelected = false;

    private void OnMouseDown()
    {     
        foreach (var item in FindObjectsOfType<SelectCharacter>()) ////// WYLACZENIE WSZYSTKICH OBIEKTOW NIE WYBRANYCH
        {
            item.isSelected = false;
            var oldColor = item.GetComponent<CharacterInSceene>().player.color;
            item.GetComponent<SpriteRenderer>().color = oldColor;
        }
        Wybrany(); /////WYBRANIE POSTACI
    }
    private void OnMouseOver()
    {
        if (isSelected == false) ////////ATAK
        {
            if (Input.GetMouseButtonDown(1))
            {
                foreach (var item in FindObjectsOfType<SelectCharacter>()) //// SZUKANIE OBIEKTY KTORY JEST AKTYWNY
                {
                    if (item.isSelected)
                    {
                        int dmg= item.GetComponent<CharacterInSceene>().OnAttack();
                        string objectName = item.GetComponent<CharacterInSceene>().name;
                        //Debug.Log(item.name + " atakuje " + gameObject.name);
                        gameObject.GetComponent<CharacterInSceene>().UnderAttack(dmg, objectName);
                    }
                }
            }
        }
       

    }

    private void Wybrany()
    {
        var oldColor = GetComponent<CharacterInSceene>().player.color;/////ZROBIENIE TRANSPARENTEGO KOLORU WYBRANEJ POSTACI
        oldColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = oldColor;
        Debug.Log("Wybrany " + gameObject.name);
        isSelected = true;
    }
}
