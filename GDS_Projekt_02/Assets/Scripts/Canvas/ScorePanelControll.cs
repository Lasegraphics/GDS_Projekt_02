using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
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
    public Text event1;
    public Text event2;

    [HideInInspector] public int damage;
    [HideInInspector] public bool isMage;

    private void Awake()
    {
    }

    public void TakeUnit(GameObject unit)
    {
        isMage = false;
        if (unit.gameObject.GetComponent<Wizard>() !=null)
        {
            isMage = true;
        }
        var unitInfo = unit.GetComponent<Unit>();
        damage = unitInfo.AttackFactor;
        sliderHp.maxValue = unitInfo.TotalHitPoints;
        sliderHp.value = unitInfo.HitPoints;
        HP.text = unitInfo.HitPoints.ToString();

        sliderArmor.maxValue = unitInfo.TotalArmorPoints;
        sliderArmor.value = unitInfo.ArmorPoints;
        armor.text = unitInfo.ArmorPoints.ToString();

        name.text = unit.name;
    }

}