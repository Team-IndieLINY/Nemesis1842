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

    private void LoadMoney()
    {
        _money = PlayerManager.Instance().Money;
    }
}
