using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShakerHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public static bool IsHovered { get; set; }
    
    [SerializeField]
    private Image _hightlightedImage;

    [SerializeField]
    private float _fadeTime = 0.2f;

    private void Awake()
    {
        IsHovered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CocktailMaterial._isDraged is true)
        {
            _hightlightedImage.DOKill();
            _hightlightedImage.DOFade(1f, _fadeTime);
        }
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hightlightedImage.DOKill();
        _hightlightedImage.DOFade(0f, _fadeTime);

        IsHovered = false;
    }
}
