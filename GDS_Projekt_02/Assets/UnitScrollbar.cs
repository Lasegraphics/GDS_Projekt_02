using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScrollbar : MonoBehaviour
{
    public Text hpUnit;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 40);
    }
    public void UpgradeTextBar(int totalHP, int HP)
    {
        slider.maxValue = totalHP;
        slider.value = HP;
        hpUnit.text = HP.ToString();
    }

}