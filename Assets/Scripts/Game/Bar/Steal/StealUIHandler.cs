using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StealUIHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private RectTransform _rectTransform;
    private Vector2 _originalPosition;
    private Vector2 _upPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
        _upPosition = _originalPosition;
        _upPosition.y += 10f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("mouse_hovered");
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPos(_upPosition, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPos(_originalPosition, 0.2f);
    }
}
