using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInSceene : MonoBehaviour
{
    public Character player;

    int health;
    int attack;
    Color color;
    //////////////////////////////////////////////////       MAIN
    private void Awake()
    {
        player = FindObjectOfType<CharacterController>().GetCharacter(name);

        GetCharacterParameters();
        transform.gameObject.tag = player.name.ToString();
        GetComponent<SpriteRenderer>().color = color;
    }

    private void GetCharacterParameters()
    {
        health = player.health;
        attack = player.attack;
        color = player.color;
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }

    /////////////////////////////////////        ATAKI
    public void UnderAttack(int dmg, string name)
    {
        if (name == player.weaknessFirst.ToString() || name == player.weaknessSecond.ToString()) ///////// ATAKUJE KONTRA
        {
            Debug.Log("Atakuje kontra");
            ; dmg *= 2;
        }
        health -= dmg;
        Debug.Log("Obecne zdrowie: " + health + " Postaci: " + gameObject.name);
        if (health < 0)
        {
            Die();
        }
    }
    public int OnAttack()
    {
        return attack;
    }
    /////////////////////////////////////        SPELE
}
