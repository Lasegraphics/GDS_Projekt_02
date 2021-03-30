using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
public class EnemyScorePanel : MonoBehaviour
{
    ScorePanelControll scorePanelControll;
   public Animator animator;
    [SerializeField] Text nameEnemy;

    [Header("Sliders")]
    [SerializeField] Slider sliderHp;
    [SerializeField] Text hp;
    //[SerializeField] Image hpBack;
    [SerializeField] Slider sliderArmor;
    [SerializeField] Text armor;
    [SerializeField] int speed=20;

    [Header("Events")]
    public Text events;
    public Text eventUnit;
    public int dodge;
    public int damageLava;
    public int heal;


    private void Awake()
    {
        scorePanelControll = FindObjectOfType<ScorePanelControll>();
    }


    public void UpgradeParameters(GameObject enemy)
    {
       
        Unit unitInfo = enemy.GetComponent<Unit>();
        nameEnemy.text = unitInfo.nameUnit;

        sliderHp.maxValue = unitInfo.TotalHitPoints;       
        sliderArmor.maxValue = unitInfo.TotalArmorPoints;

        if (scorePanelControll.isMage || unitInfo.ArmorPoints == 0 )
        {
            animator.SetBool("BlinkHp",true);
            animator.SetBool("BlinkArmor", false);
            sliderHp.value = unitInfo.HitPoints;
            hp.text = unitInfo.HitPoints.ToString() +" - " + scorePanelControll.damage;

            sliderArmor.value = unitInfo.ArmorPoints;
            armor.text = unitInfo.ArmorPoints.ToString(); 
        }
        else
        {
            animator.SetBool("BlinkArmor", true);
            animator.SetBool("BlinkHp", false);
            sliderHp.value = unitInfo.HitPoints;
            sliderArmor.value = unitInfo.ArmorPoints;
            hp.text = unitInfo.HitPoints.ToString();
            if (unitInfo.ArmorPoints<=0)
            {
                armor.text = "0";
            }
            else
            {
                armor.text = unitInfo.ArmorPoints.ToString() + " - " + scorePanelControll.damage;
            }
           
        }  
    }
    public void UpgadeParameters(Unit unit)
    {
        events.text = "";
        eventUnit.text = "";
        if (unit.Cell.Forest)
        {
            events.text = "EVENTS";
            eventUnit.text = ("FOREST- " + dodge + "% CHANCE TO DODGE");
        }

        if (unit.Cell.Spikes)
        {
            events.text = "EVENTS";
            eventUnit.text = ("LAVA- " + damageLava + " DAMAGE AT THE BEGINNING OF NEXT TURN ");
        }
        if (unit.Cell.Temple)
        {
            events.text = "EVENTS";
            eventUnit.text = ("TEMPLE - HEAL " + heal + " AT THE END OF CURRENT TURN");
        }

    }
    public void ChangeHpSlidder(Unit unit)
    {
        if (sliderHp.value != unit.HitPoints )
        {
            sliderHp.value = Mathf.MoveTowards(sliderHp.value, unit.HitPoints, speed * Time.deltaTime);
            hp.text = unit.HitPoints.ToString();
        }
    }
   
    public void ChangeArmorSlidder(Unit unit)
    {
        if (sliderArmor.value != unit.ArmorPoints)
        {
            sliderArmor.value = Mathf.MoveTowards(sliderArmor.value, unit.ArmorPoints, speed * Time.deltaTime);
            armor.text = unit.ArmorPoints.ToString();
        }
    }
   
    public void RestEvents()
    {
        events.text = "";
        eventUnit.text = "";
    }
}
