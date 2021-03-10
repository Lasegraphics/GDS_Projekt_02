using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Units;
public class EnemyScorePanel : MonoBehaviour
{
    [SerializeField] Text name;   
    [SerializeField] Text damage;
    [SerializeField] Slider slider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void UpgradeParameters(GameObject enemy)
    {
        Unit unitParameters = enemy.GetComponent<Unit>();
        name.text = enemy.name;
        damage.text = unitParameters.AttackFactor.ToString();
        slider.maxValue = unitParameters.TotalHitPoints;
        slider.value = unitParameters.HitPoints;
    }
}
