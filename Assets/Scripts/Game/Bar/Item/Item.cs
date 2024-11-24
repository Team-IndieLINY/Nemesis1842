using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ItemData _itemData;
    public ItemData ItemData => _itemData;
    
    private int _itemAmount;
    public int ItemAmount => _itemAmount;

    [SerializeField]
    private ItemTooltipUI _itemTooltipUI;

    [SerializeField]
    private AlcoholController _alcoholController;

    [SerializeField]
    private TextMeshProUGUI _usingItemText;

    [SerializeField]
    private CanvasGroup _usingItemSignCanvasGroup;

    private ItemUI _itemUI;

    private void Awake()
    {
        _itemUI = GetComponent<ItemUI>();
    }

    private void Start()
    {
        _itemAmount = InventoryManager.Instance().ItemAmounts[(int)_itemData.ItemType];
        _itemUI.UpdateItemUI();
    }

    public void IncreaseAmount(int amount)
    {
        _itemAmount += amount;
        
        InventoryManager.Instance().AddItem(_itemData.ItemType,amount);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_alcoholController.CurrentItem == this)
        {
            _itemAmount++;
            InventoryManager.Instance().AddItem(_itemData.ItemType,1);
            _itemUI.UpdateItemUI();
            
            _alcoholController.EquipItem(null);
            
            _usingItemSignCanvasGroup.DOKill();
            _usingItemText.text = "<color #c89e38>\" " + _itemData.ItemName + "\"</color>를 사용을 취소합니다.";

            Sequence sequence2 = DOTween.Sequence();
            sequence2.Append(_usingItemSignCanvasGroup.DOFade(1f, 0.3f))
                .AppendInterval(0.5f)
                .Append(_usingItemSignCanvasGroup.DOFade(0f, 0.3f));
            
            return;
        }

        if (_itemAmount == 0)
        {
            return;
        }
        
        _itemAmount--;
        
        InventoryManager.Instance().AddItem(_itemData.ItemType,-1);
        _itemUI.UpdateItemUI();
        _alcoholController.EquipItem(this);

        _usingItemSignCanvasGroup.DOKill();
        _usingItemText.text = "<color #c89e38>\" " + _itemData.ItemName + "\"</color>를 사용합니다.";

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_usingItemSignCanvasGroup.DOFade(1f, 0.3f))
            .AppendInterval(0.5f)
            .Append(_usingItemSignCanvasGroup.DOFade(0f, 0.3f));

    }
}