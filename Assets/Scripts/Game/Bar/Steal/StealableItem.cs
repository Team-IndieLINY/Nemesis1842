using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StealableItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public enum EStealableItemType
    {
        Money,
        Information
    }
    
    [SerializeField]
    private EStealableItemType _stealableItemType;
    
    [SerializeField]
    private int _moneyAmount;
    
    private RectTransform _rectTransform;
    private Vector2 _originalPosition;
    private Vector2 _upPosition;

    private RectTransform _belongingRectTransform;
    private float _mousePositionXDiff;
    private float _mousePositionYDiff;
    private int _originalChildIndex;
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
        _upPosition = _originalPosition;
        _upPosition.y += 50f;
        
        _belongingRectTransform = _rectTransform.parent.GetComponent<RectTransform>();
        
        _mousePositionXDiff = _belongingRectTransform.sizeDelta.x / 2;
        _mousePositionYDiff = _belongingRectTransform.sizeDelta.y / 2;

        _originalChildIndex = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOAnchorPos(_upPosition, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_image.raycastTarget == false)
        {
            return;
        }
        _rectTransform.DOAnchorPos(_originalPosition, 0.2f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.DOKill();
        transform.SetAsLastSibling();

        _image.raycastTarget = false;

        _rectTransform.anchoredPosition = new Vector2(
            Input.mousePosition.x - 960 + _mousePositionXDiff,
            Input.mousePosition.y - 540 + _mousePositionYDiff);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = new Vector2(
            Input.mousePosition.x - 960 + _mousePositionXDiff,
            Input.mousePosition.y - 540 + _mousePositionYDiff);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetSiblingIndex(_originalChildIndex);
        _rectTransform.DOAnchorPos(_originalPosition, 0.2f);

        if (StealInventoryHandler.Inst.IsHovered)
        {
            _rectTransform.DOKill();
            if (_stealableItemType == EStealableItemType.Information)
            {
                
            }
            else if(_stealableItemType == EStealableItemType.Money)
            {
                
            }
            Destroy(gameObject);
        }
    }
}
