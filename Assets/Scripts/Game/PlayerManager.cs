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
    
    public int AIEnhancerItemUsingCount { get; set; }
    public int CoolerItemUsingCount { get; set; }

    public bool IsNewItemDotActivated { get; set; }
    

    public void SaveMoneyData(int money)
    {
        _money = money;
    }

    public void ResetPlayerData()
    {
        _money = 0;
        AIEnhancerItemUsingCount = 0;
        CoolerItemUsingCount = 0;
        IsNewItemDotActivated = false;
    }
}
