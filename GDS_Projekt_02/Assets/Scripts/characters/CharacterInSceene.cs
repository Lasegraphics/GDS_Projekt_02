using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInSceene : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Character player;
    GameObject enemy;

    int health;
    int damage;
    
    private void Awake()
    {
        string objectName = name;
        characterController = FindObjectOfType<CharacterController>();
        player = characterController.GetCharacter(objectName);
        transform.gameObject.tag = player.tag.ToString();

        health = player.health;
        damage = player.attack;
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = player.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag =="Mage")
        {
            var dmg = collision.gameObject.GetComponent<CharacterInSceene>().OnAttack();
            UnderAttack(dmg);
        }
        
    }
    void UnderAttack(int dmg)
    {
        health -= dmg;
        Debug.Log(health);
        if (health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("umarlem i nie wstane");
    }
   int OnAttack()
    {
        return player.attack;
    }
}
