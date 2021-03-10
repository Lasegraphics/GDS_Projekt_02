using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
public class EnemyScorePanel : MonoBehaviour
{
    ScorePanelControll scorePanelControll;

    [SerializeField] Text name;

    [Header("Sliders")]
    [SerializeField] Slider sliderHp;
    [SerializeField] Text hp;
    [SerializeField] Slider sliderArmor;
    [SerializeField] Text armor;

    [Header("Events")]
    [SerializeField] Text event1;
    [SerializeField] Text event2;

    [Header("ShowDmg")]
    [SerializeField] Text firstSegment;
    [SerializeField] Text secondSegment;
    [SerializeField] Text result;

    private void Awake()
    {
        scorePanelControll = FindObjectOfType<ScorePanelControll>();
    }

    void Update()
    {
        
    }
    public void UpgradeParameters(GameObject enemy)
    {
        Unit unitInfo = enemy.GetComponent<Unit>();
        name.text = enemy.name;

        sliderHp.maxValue = unitInfo.TotalHitPoints;
        sliderHp.value = unitInfo.HitPoints;
        hp.text = unitInfo.HitPoints.ToString();

        sliderArmor.maxValue = unitInfo.TotalArmorPoints;
        sliderArmor.value = unitInfo.ArmorPoints;
        armor.text = unitInfo.ArmorPoints.ToString();

        firstSegment.text = ("("+ unitInfo.ArmorPoints+"+"+ unitInfo.HitPoints+")");
        secondSegment.text = (scorePanelControll.damage.text+"=");
        result.text = ((unitInfo.ArmorPoints + unitInfo.HitPoints) - int.Parse(scorePanelControll.damage.text)).ToString();

    }
}
