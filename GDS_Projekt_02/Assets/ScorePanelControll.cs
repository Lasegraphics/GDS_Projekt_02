using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Cells;
using GridPack.Grid.GridStates;
using GridPack.Grid.UnitGenerators;
using GridPack.Players;
using GridPack.Units;
using GridPack.Grid;


public class ScorePanelControll : MonoBehaviour
{
    public Text name;
    public Slider scrollbar;
    public Text HP;

    public Button attackButton;
    public Button speelButton;
    public Button moveButton;

    public Text descriptions;
    public Text damage;


    private void Awake()
    {
    }

    public void TakeUnit(GameObject unit)
    {
        var unitInfo = unit.GetComponent<Unit>();

        scrollbar.maxValue = unitInfo.TotalHitPoints;
        scrollbar.value = unitInfo.HitPoints;
        HP.text = unitInfo.HitPoints.ToString();

        name.text = unit.name;
        damage.text = unitInfo.AttackFactor.ToString();
    }

    void Update()
    {

    }
}