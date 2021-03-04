using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridPack.Cells;
using GridPack.Grid.GridStates;
using GridPack.Grid.UnitGenerators;
using GridPack.Players;
using GridPack.Units;
public class ScorePanelControll : MonoBehaviour
{
    public Text name;
    public Scrollbar scrollbar;

    public Button attackButton;
    public Button speelButton;
    public Button moveButton;

    public Text descriptions;
    public Text maxDmg;
    public Text minDmg;
    public Button AcepptButton;
    public Button CancelButton;

    
    void Start()
    {
        
    }
   public void TakeUnit(Transform unit)
    {
      int cos =  unit.GetComponent<Unit>().AttackMax;
        Debug.Log(cos);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
