using System.Collections;
using GridPack.Cells;
using GridPack.Units;
using UnityEngine;

namespace Scripts.characters
{
    public class CharacterInSceene : MonoBehaviour
    {
        public Character player;
        int health;
        int armor;
        Color color;
        public bool ignoreArmor;

        //////////////////////////////////////////////////       MAIN
        private void Awake()
        {
            gameObject.name = gameObject.tag;
            player = FindObjectOfType<CharacterController>().GetCharacter(name);

            GetCharacterParameters();
            GetComponent<SpriteRenderer>().color = color;
        }

        private void GetCharacterParameters()
        {
            armor = player.armor;
            ignoreArmor = player.ignoreArmor;
            health = player.health;
            color = player.color;
        }
        public void Die()
        {
            gameObject.SetActive(false);
        }

        /////////////////////////////////////        ATAKI
        public void UnderAttack(int dmg, string name, bool ignoreArmor)
        {
            if (armor > 0)
            {
                if (ignoreArmor)
                {
                    health -= dmg;
                }
                else
                {
                    armor -= dmg;
                }
            }
            else
            {
                health -= dmg;
            }

            if (health < 0)
            {
                Die();
            }
            Debug.Log(name + " ATAKUJE " + player.name + " obecne zdrowie: " + health+ " obecny armor: " + armor);
        }
        public int OnAttack()
        {
            return player.attack();
        }
        /////////////////////////////////////        SPELE
    }
}
