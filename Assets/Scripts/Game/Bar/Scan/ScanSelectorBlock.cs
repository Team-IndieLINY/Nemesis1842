using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScanSelectorBlock : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField]
    private ScanManager.EScanType _scanType;

    public void SetCurrentScanType()
    {
        ScanManager.Inst.CurrentScanType = _scanType;
        ScanManager.Inst.FadeInScanSelectorBackground();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOKill();
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
