using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class CocktailMaterial : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField]
    protected CocktailMakingManager _cocktailMakingManager;
    [SerializeField]
    private Transform _movingCocktailMaterialGroupTransform;
    
    private RectTransform _rectTransform;
    private Image _image;
    private Vector3 _originalPosition;
    private Transform _originalParent;
    private void Awake()
    {
        _originalParent = transform.parent;
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _originalPosition = _rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_movingCocktailMaterialGroupTransform);
        
        _image.raycastTarget = false;
        
        _rectTransform.anchoredPosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ShakerHandler.IsHovered)
        {
            SetCocktail();
        }
        
        transform.SetParent(_originalParent);
        _rectTransform.anchoredPosition = _originalPosition;
        _image.raycastTarget = true;


    }

    public abstract void SetCocktail();
}
