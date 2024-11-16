using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StealableItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IPointerMoveHandler
{
    [SerializeField]
    private StealableItemData _stealableItemData;
    
    private RectTransform _rectTransform;
    private Vector2 _originalPosition;
    private Vector2 _upPosition;
    
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
        _upPosition = _originalPosition;
        _upPosition.y += 50f;
        _image.sprite = _stealableItemData.ItemSprite;
        _image.SetNativeSize();
        
        var sizeDelta = _image.rectTransform.sizeDelta;
        sizeDelta =
            new Vector2(sizeDelta.x * 2, sizeDelta.y * 2);
        _image.rectTransform.sizeDelta = sizeDelta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_stealableItemData is InformationItemData informationItemData)
        {
            StolenManager.Inst.ShowStealableItemNameTag(informationItemData.ItemName);
        }
        else if(_stealableItemData is MoneyItemData moneyItemData)
        {
            StolenManager.Inst.ShowStealableItemNameTag(moneyItemData.Money + "$");
        }
        
        _rectTransform.DOAnchorPos(_upPosition, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StolenManager.Inst.HideStealableItemNameTag();
        
        if (_image.raycastTarget == false)
        {
            return;
        }
        
        _rectTransform.DOAnchorPos(_originalPosition, 0.2f);
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
        StolenManager.Inst.UpdateStealableItemNameTagPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StolenManager.Inst.InActivateStealableItems();
        StolenManager.Inst.HideStealableItemNameTag();

        _rectTransform.DOKill();
        _rectTransform.anchoredPosition = _upPosition;
        
        Vector2 moreUpPosition = new Vector2(_upPosition.x, _upPosition.y + 60f);

        _rectTransform.DOAnchorPos(moreUpPosition, 0.5f);
        _image.DOFade(0f, 0.5f)
            .OnKill(() =>
            {
                if (_stealableItemData is MoneyItemData moneyItemData)
                {
                    StolenManager.Inst.StealMoney(moneyItemData.Money);
                }
                else if (_stealableItemData is InformationItemData informationItemData)
                {
                    StolenManager.Inst.StealInformationItem(informationItemData);
                }
                StolenManager.Inst.ActivateStealableItems();
                Destroy(gameObject);
            });
    }

    public void InActivateStealableItem()
    {
        _image.raycastTarget = false;
    }
    
    public void ActivateStealableItem()
    {
        _image.raycastTarget = true;
    }
}
