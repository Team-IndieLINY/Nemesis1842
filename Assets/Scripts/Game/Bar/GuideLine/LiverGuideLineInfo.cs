using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LiverGuideLineInfo : GuideLineInfo,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image _checkImage;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _checkImage.DOFillAmount(1f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_image.raycastTarget == false)
        {
            return;
        }
        
        _checkImage.DOKill();
        _checkImage.fillAmount = 0f;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _checkImage.fillAmount = 0f;
        
        _checkImage.DOFillAmount(1f, 0.3f);
        
        StartCoroutine(WaitEvaluationScanCoroutine());
    }
    
    public override void ResetGuideLineInfo()
    {
        _image.raycastTarget = true;
        _checkImage.fillAmount = 0f;
    }
}