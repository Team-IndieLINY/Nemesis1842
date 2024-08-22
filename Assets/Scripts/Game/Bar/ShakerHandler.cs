using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakerHandler : MonoBehaviour,IPointerEnterHandler,IPointerMoveHandler,IPointerExitHandler
{
    [SerializeField]
    private ShakerContentTooltip _shakerContentTooltip;

    private RectTransform _shakerContentTooltipRectTransform;

    private void Awake()
    {
        _shakerContentTooltipRectTransform = _shakerContentTooltip.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _shakerContentTooltip.ShowTooltip();
        _shakerContentTooltipRectTransform.anchoredPosition = Input.mousePosition;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        _shakerContentTooltipRectTransform.anchoredPosition = Input.mousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _shakerContentTooltip.HideTooltip();
    }
}
