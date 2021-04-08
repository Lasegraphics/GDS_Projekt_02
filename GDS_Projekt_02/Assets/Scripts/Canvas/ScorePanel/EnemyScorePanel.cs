using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
using GridPack.SceneScripts;
using TMPro;
public class EnemyScorePanel : MonoBehaviour
{
    [Header("Animator i nazwa")]
    [SerializeField] Text nameEnemy;
    [SerializeField] private Animator animator;
    [SerializeField] public ScorePanelControll scorePanelControll;


    [Header("Sliders")]
    [SerializeField] private Slider sliderHp;
    [SerializeField] private Text hp;
    [SerializeField] private Slider sliderArmor;
    [SerializeField] private GameObject hideArmor;

    [SerializeField] private int speed=20;


    [Header("Events")]
    [SerializeField] private Text events;
    [SerializeField] private TextMeshProUGUI eventUnit;
    [SerializeField] private int dodge;
    [SerializeField] private int damageLava;
    [SerializeField] private int heal;
    [SerializeField] private TextMeshProUGUI movmentText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] public GameObject hide;

    [HideInInspector] public bool isBlinging;
    [HideInInspector] public Unit unitInfo;

    public void UpgradeParameters(GameObject enemy)
    {
        if (enemy.GetComponent<ArmoredEntity>() !=null)
        {
            hideArmor.SetActive(false);
        }
        else
        {
            hideArmor.SetActive(true);
        }
        unitInfo = enemy.GetComponent<Unit>();
        nameEnemy.text = unitInfo.nameUnit;

        sliderHp.maxValue = unitInfo.TotalHitPoints;
        sliderArmor.maxValue = 1;

        movmentText.text = ("MOVEMENT:  " + unitInfo.MovementPoints);
        rangeText.text = ("RANGE:  "+unitInfo.AttackRange);
        DamageText.text = ("DAMAGE:  " + unitInfo.AttackFactor);

        if (scorePanelControll.isMage || unitInfo.ArmorPoints == 0)
        {
            if (unitInfo.isBlinking)
            {
                animator.SetBool("BlinkHp", true);
                animator.SetBool("BlinkArmor", false);
            }

            sliderHp.value = unitInfo.HitPoints;
            hp.text = unitInfo.HitPoints.ToString() + " - " + scorePanelControll.damage;
            sliderArmor.value = unitInfo.ArmorPoints;
        }
        else
        {
            if (unitInfo.isBlinking)
            {
                animator.SetBool("BlinkArmor", true);
                animator.SetBool("BlinkHp", false);
            }

            sliderHp.value = unitInfo.HitPoints;
            sliderArmor.value = unitInfo.ArmorPoints;
        }
    }

    public void UpgradeDmgMagic(Unit unit)
    {
        hp.text = unitInfo.HitPoints.ToString() + " - " + scorePanelControll.damage*2;
    }
    public void Blinking()
    {
        if (scorePanelControll.isMage || unitInfo.ArmorPoints == 0)
        {
            animator.SetBool("BlinkHp", true);
            animator.SetBool("BlinkArmor", false);
        }
        else
        {
            animator.SetBool("BlinkArmor", true);
            animator.SetBool("BlinkHp", false);
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
        }
    }
   
    public void RestEvents()
    {
        events.text = "";
        eventUnit.text = "";
    }
}
