using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StealableItemData/InformationItemData", fileName = "InformationItemData", order = Int32.MaxValue)]
public class InformationItemData : StealableItemData
{
    [SerializeField]
    private string _itemName;

    public string ItemName => _itemName;
    
    [SerializeField]
    private string _itemDescription;

    public string ItemDescription => _itemDescription;
}
