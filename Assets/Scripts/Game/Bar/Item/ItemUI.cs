using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemAmountText;

    private Item _item;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }

    private void Start()
    {
        UpdateItemUI();
    }

    public void UpdateItemUI()
    {
        _itemAmountText.text = "x" + _item.ItemAmount;
    }
}
