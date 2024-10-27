using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    private static InventoryManager _instance;

    public static InventoryManager Instance()
    {
        if (_instance == null)
        {
            _instance = new InventoryManager();
        }

        return _instance;
    }
    
    private InformationItemData[] _informationItemDatas = new InformationItemData[5];
    public InformationItemData[] InformationItemDatas => _informationItemDatas;

    public void SaveInventoryData(InformationItemData[] informationItemDatas)
    {
        _informationItemDatas = informationItemDatas;
    }

    public void ResetInventoryData()
    {
        _informationItemDatas = new InformationItemData[5];
    }
}