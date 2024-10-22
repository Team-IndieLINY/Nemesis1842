using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData",fileName = "ItemData",order = Int32.MaxValue)]
public class ItemData : ScriptableObject
{
    public enum EItemType
    {
        AlcoholAmplifier,
        AIEnhancer,
        Cooler
    }

    [SerializeField]
    private Sprite _itemSprite;

    public Sprite ItemSprite => _itemSprite;
    
    [SerializeField]
    private string _itemName;

    public string ItemName => _itemName;
    
    [SerializeField]
    private string _itemDescription;

    public string ItemDescription => _itemDescription;

    [SerializeField]
    private EItemType _itemType;

    public EItemType ItemType => _itemType;
}
