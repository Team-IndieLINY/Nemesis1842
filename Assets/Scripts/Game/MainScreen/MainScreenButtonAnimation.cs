using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainScreenButtonAnimation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Vector2 _movePosition;

    private Vector2 _originPosition;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _originPosition = _rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPos(_movePosition, 0.3f);
        AudioManager.Inst.PlaySFX("mouse_hovered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPos(_originPosition, 0.3f);
    }
}
