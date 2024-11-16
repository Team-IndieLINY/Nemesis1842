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

    private InventoryManager()
    {
        _itemAmounts = new int[3];

        for (int i = 0; i < _itemAmounts.Length; i++)
        {
            _itemAmounts[i] = 0;
        }
    }

    private InformationItemData[] _informationItemDatas = new InformationItemData[5];
    public InformationItemData[] InformationItemDatas => _informationItemDatas;

    private int[] _itemAmounts = new int[3];
    public int[] ItemAmounts => _itemAmounts;
    
    public void SaveInventoryData(InformationItemData[] informationItemDatas)
    {
        _informationItemDatas = informationItemDatas;
    }
    

    public void ResetInventoryData()
    {
        _informationItemDatas = new InformationItemData[5];
    }

    public void AddItem(ItemData.EItemType itemType, int amount)
    {
        _itemAmounts[(int)itemType] += amount;
    }
    
    public void ResetItems()
    {
        _itemAmounts = new int[3];
    }
}