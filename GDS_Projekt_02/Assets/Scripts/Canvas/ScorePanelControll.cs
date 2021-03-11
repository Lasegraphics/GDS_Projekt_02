
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;



public class ScorePanelControll : MonoBehaviour
{
    [Header("Static")]
    public Text name;
    public Text damage;

    [Header("Sliders")]
    public Slider sliderHp;
    public Text HP;
    public Slider sliderArmor;
    public Text armor;

    [Header("Events")]
    public Text event1;
    public Text event2;

    private void Awake()
    {
    }

    public void TakeUnit(GameObject unit)
    {
        var unitInfo = unit.GetComponent<Unit>();

        sliderHp.maxValue = unitInfo.TotalHitPoints;
        sliderHp.value = unitInfo.HitPoints;
        HP.text = unitInfo.HitPoints.ToString();

        sliderArmor.maxValue = unitInfo.TotalArmorPoints;
        sliderArmor.value = unitInfo.ArmorPoints;
        armor.text = unitInfo.ArmorPoints.ToString();

        name.text = unit.name;
        damage.text = unitInfo.AttackFactor.ToString();
    }

    void Update()
    {

    }
}