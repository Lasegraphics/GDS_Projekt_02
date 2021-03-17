using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
public class EnemyScorePanel : MonoBehaviour
{
    ScorePanelControll scorePanelControll;

    [SerializeField] Text nameEnemy;

    [Header("Sliders")]
    [SerializeField] Slider sliderHp;
    [SerializeField] Text hp;
    //[SerializeField] Image hpBack;
    [SerializeField] Slider sliderArmor;
    [SerializeField] Text armor;

    [Header("Events")]
    [SerializeField] Text event1;
    [SerializeField] Text event2;


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
            sliderHp.value = unitInfo.HitPoints - scorePanelControll.damage;
            hp.text = unitInfo.HitPoints.ToString() +" - " + scorePanelControll.damage;

            sliderArmor.value = unitInfo.ArmorPoints;
            armor.text = unitInfo.ArmorPoints.ToString(); 
        }
        else
        {
            sliderHp.value = unitInfo.HitPoints;
            hp.text = unitInfo.HitPoints.ToString();

            sliderArmor.value = unitInfo.ArmorPoints - scorePanelControll.damage;
            armor.text = unitInfo.ArmorPoints.ToString() + " - " +scorePanelControll.damage;
        }
       

     
    }
}
