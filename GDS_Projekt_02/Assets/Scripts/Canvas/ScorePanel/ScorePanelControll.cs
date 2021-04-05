using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
using GridPack.Cells;
using GridPack.SceneScripts;
using TMPro;


public class ScorePanelControll : MonoBehaviour
{
    [Header("Static")]
    [SerializeField] private Text nameUnit;
   
    [Header("Sliders")]
    [SerializeField] private Slider sliderHp;
    [SerializeField] private Text HP;
    [SerializeField] private Slider sliderArmor;
    [SerializeField] private GameObject hideArmor;

    [Header("Events")]
    [SerializeField] private Text events;
    [SerializeField] private TextMeshProUGUI eventUnit;
    [SerializeField] private int dodge;
    [SerializeField] private int damageLava;
    [SerializeField] private int heal;
    [SerializeField] private TextMeshProUGUI movmentText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI DamageText;

    [HideInInspector] public int damage;
    [HideInInspector] public bool isMage;

    public void TakeUnit(GameObject unit)
    {
        if (unit.GetComponent<ArmoredEntity>() != null)
        {
            hideArmor.SetActive(false);
        }
        else
        {
            hideArmor.SetActive(true);
        }

        movmentText.text = ("MOVEMENT:  " + unit.GetComponent<Unit>().MovementPoints);
        rangeText.text = ("RANGE:  " + unit.GetComponent<Unit>().AttackRange);
        DamageText.text = ("DAMAGE:  " + unit.GetComponent<Unit>().AttackFactor);
        if (unit.GetComponent<Wizard>()!=null)
        {
            DamageText.text =("DAMAGE:  "+unit.GetComponent<Unit>().AttackFactor/2);
        }
        if (unit.GetComponent<Wizard>()!=null && unit.GetComponent<Unit>().TotalMovementPoints == unit.GetComponent<Unit>().MovementPoints)
        {
            DamageText.text =("DAMAGE:  "+unit.GetComponent<Unit>().AttackFactor);
        }
        if (FindObjectOfType<UiManager>().isStart == false)
        {
            isMage = false;
            if (unit.gameObject.GetComponent<Wizard>() != null)
            {
                isMage = true;
            }
            var unitInfo = unit.GetComponent<Unit>();         

            nameUnit.text = unitInfo.nameUnit;
            damage = unitInfo.AttackFactor;

            sliderHp.maxValue = unitInfo.TotalHitPoints;
            sliderHp.value = unitInfo.HitPoints;
            HP.text = unitInfo.HitPoints.ToString();

            sliderArmor.maxValue = 1;
            sliderArmor.value = unitInfo.ArmorPoints;        
        }      
    }
   public void UpgradeMovment(Unit unit)
    {
        movmentText.text = ("MOVEMENT:  " + unit.MovementPoints);
        DamageText.text =("DAMAGE:  "+unit.AttackFactor);
        if (unit.GetComponent<Wizard>()!=null && unit.TotalMovementPoints == unit.MovementPoints)
        {
            DamageText.text =("DAMAGE:  "+unit.AttackFactor*2);
        }

    }

    public void UpgadeParameters(Unit unit)
    {
        events.text = "EVENTS";
        if (unit.Cell.Forest)
        {           
            eventUnit.text = ("FOREST- "+dodge+"% CHANCE TO DODGE");
        }
        if (unit.Cell.Spikes)
        {
            eventUnit.text = ( "LAVA- "+damageLava+" DAMAGE AT THE BEGINNING OF NEXT TURN ");
        }
        if (unit.Cell.Temple)
        {
            eventUnit.text = ("TEMPLE - HEAL "+heal+" AT THE END OF CURRENT TURN");
        }
       
    }
    public void RestEvents()
    {
        events.text = "";
        eventUnit.text = "";
    }
}