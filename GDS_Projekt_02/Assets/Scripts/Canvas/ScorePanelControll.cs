using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
using GridPack.Cells;
using GridPack.SceneScripts;


public class ScorePanelControll : MonoBehaviour
{
    [Header("Static")]
    public Text name;
   
    [Header("Sliders")]
    public Slider sliderHp;
    public Text HP;
    public Slider sliderArmor;
    public Text armor;

    [Header("Events")]
    public Text events;
    public Text eventUnit;
    public int dodge;
    public int damageLava;
    public int heal;

    [HideInInspector] public int damage;
    [HideInInspector] public bool isMage;

    private void Awake()
    {
    }

    public void TakeUnit(GameObject unit)
    {
        if (FindObjectOfType<UiManager>().isStart == false)
        {
            isMage = false;
            if (unit.gameObject.GetComponent<Wizard>() != null)
            {
                isMage = true;
            }
            var unitInfo = unit.GetComponent<Unit>();

           

            name.text = unitInfo.nameUnit;
            damage = unitInfo.AttackFactor;

            sliderHp.maxValue = unitInfo.TotalHitPoints;
            sliderHp.value = unitInfo.HitPoints;
            HP.text = unitInfo.HitPoints.ToString();

            sliderArmor.maxValue = unitInfo.TotalArmorPoints;
            sliderArmor.value = unitInfo.ArmorPoints;
            armor.text = unitInfo.ArmorPoints.ToString();
           
          
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