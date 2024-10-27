using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StealableItemData/MoneyItemData", fileName = "MoneyItemData", order = Int32.MaxValue)]
public class MoneyItemData : StealableItemData
{
    [SerializeField]
    private int money;

    public int Money => money;
}
