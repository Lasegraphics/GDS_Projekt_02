using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInSceene : MonoBehaviour
{
    public Character player;

    int health;
    int damage;
    
    private void Awake()
    {
        string objectName = name;
        player = FindObjectOfType<CharacterController>().GetCharacter(objectName);
        transform.gameObject.tag = player.name.ToString();

        health = player.health;
        damage = player.attack;
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = player.color;
    }

   public void UnderAttack(int dmg, string name)
    {
        if (name == player.weaknessFirst.ToString() || name == player.weaknessSecond.ToString()) ///////// ATAKUJE KONTRA
        {
            Debug.Log("Atakuje kontra");
;            dmg *= 2;
        }
        health -= dmg;
        Debug.Log("Obecne zdrowie: "+health+" Postaci: "+ gameObject.name);
        if (health < 0)
        {
            Die();
        }
    }

   public void Die()
    {
        gameObject.SetActive(false);
    }
  public int OnAttack()
    {
        return damage;
    }
}
