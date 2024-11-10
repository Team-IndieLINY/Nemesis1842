using System;
using UnityEngine;

[RequireComponent(typeof(InventoryUI))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Slot[] _slots;
    
    private InventoryUI _inventoryUI;
    private Slot _currentSelectedSlot;
    public Slot CurrentSelectedSlot => _currentSelectedSlot;

    private void Awake()
    {
        _inventoryUI = GetComponent<InventoryUI>();
    }

    private void Start()
    {
        for(int i=0;i< InventoryManager.Instance().InformationItemDatas.Length;i++)
        {
            AddItem(InventoryManager.Instance().InformationItemDatas[i]);
        }
        
        gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsEmpty() is false)
            {
                slot.ActivateSlot();
            }
        }

        PopUpUIManager.Inst.OpenUI(_inventoryUI);
    }
    
    public void CloseInventory()
    {
        PopUpUIManager.Inst.CloseUI();
    }
    
    public void OpenStealInventory()
    {
        foreach (var slot in _slots)
        {
            slot.InActivateSlot();
        }
        _inventoryUI.OpenStealInventoryUI();
    }
    
    public void CloseStealInventory()
    {
        _inventoryUI.CloseStealInventoryUI();
    }

    public void AddItem(InformationItemData informationItemData)
    {
        foreach (var slot in _slots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(informationItemData);
                return;
            }
        }
    }

    public void SelectSlot(Slot selectedSlot)
    {
        foreach (var slot in _slots)
        {
            slot.UnSelectSlot();
        }

        _currentSelectedSlot = selectedSlot;
            
        _currentSelectedSlot.SelectSlot();
        _inventoryUI.UpdateItemDescriptionPanel();
    }

    public void SaveInventoryData()
    {
        InformationItemData[] informationItemDatas = new InformationItemData[_slots.Length];
        
        for(int i=0;i<_slots.Length;i++)
        {
            informationItemDatas[i] = _slots[i].InformationItemData;
        }
        
        InventoryManager.Instance().SaveInventoryData(informationItemDatas);   
    }
}
