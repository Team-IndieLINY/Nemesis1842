using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ItemData _itemData;
    public ItemData ItemData => _itemData;

    [SerializeField]
    private GameObject _itemGO;
    
    
    [SerializeField]
    private int _itemAmount = 2;
    public int ItemAmount => _itemAmount;

    [SerializeField]
    private ItemHandler _itemHandler;

    [SerializeField]
    private ItemTooltipUI _itemTooltipUI;

    private ItemUI _itemUI;

    private void Awake()
    {
        _itemUI = GetComponent<ItemUI>();
    }

    public void SetItem(int amount)
    {
        _itemAmount = amount;
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_itemAmount <= 0)
        {
            return;
        }
        
        _itemAmount--;
        _itemUI.UpdateItemUI();
        
        _itemHandler.SetItem(this);
        
        ItemHandler.IsClicked = true;
        _itemGO.SetActive(true);
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        _itemGO.transform.position = mousePosition;
        

    }

    public void IncreaseAmount(int amount)
    {
        _itemAmount += amount;
        _itemUI.UpdateItemUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemTooltipUI.UpdateTooltipUI(_itemData);
        _itemTooltipUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemTooltipUI.gameObject.SetActive(false);
    }
}