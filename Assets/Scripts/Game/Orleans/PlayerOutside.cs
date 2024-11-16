using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutside : MonoBehaviour
{
    [SerializeField]
    private MoneyUI _moneyUI;
    
    private int _money;
    public int Money => _money;
    
    private void Awake()
    {
        LoadMoney();
        _moneyUI.UpdateMoneyUI();
    }

    public void EarnMoney(int amount)
    {
        int tempMoney = _money;
        _money = Mathf.Clamp(_money + amount, 0, Int32.MaxValue);
        
        StartCoroutine(_moneyUI.AnimateEarningMoneyText(tempMoney, _money));
    }
    private void LoadMoney()
    {
        _money = PlayerManager.Instance().Money;
    }
}
