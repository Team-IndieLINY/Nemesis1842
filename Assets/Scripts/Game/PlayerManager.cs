using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private static PlayerManager _instance;

    public static PlayerManager Instance()
    {
        if (_instance == null)
        {
            _instance = new PlayerManager();
        }

        return _instance;
    }

    private int _money;
    public int Money => _money;

    public void SaveMoneyData(int money)
    {
        _money = money;
    }
}
