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
    public Scrollbar scrollbar;
    public Text HP;

    public Button attackButton;
    public Button speelButton;
    public Button moveButton;

    public Text descriptions;
    public Text maxDmg;
    public Text minDmg;
    public Button AcepptButton;
    public Button CancelButton;

    private void Awake()
    {
    }
   
    public void TakeUnit(GameObject unit)
    {
        var unitInfo = unit.GetComponent<Unit>();
        name.text = unit.name;
        maxDmg.text = unitInfo.AttackMax.ToString();
        minDmg.text = unitInfo.AttackMin.ToString();
        HP.text = unitInfo.HitPoints.ToString();
    }
   
    void Update()
    {

    }
}

