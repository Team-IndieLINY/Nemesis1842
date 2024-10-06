using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCardSlot : MonoBehaviour
{
    private ScanCard _scanCard;
    public ScanCard ScanCard => _scanCard;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public bool IsEmpty()
    {
        return _scanCard == null;
    }

    public void AttachCard(ScanCard scanCard)
    {
        scanCard.IsAttached = true;
        _scanCard = scanCard;
        _scanCard.transform.SetParent(transform);
        
        var sizeDelta = _rectTransform.sizeDelta;
        _scanCard.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f);
    }

    public void RemoveCard()
    {
        if (IsEmpty())
        {
            return;
        }
        ScanCardPool.Inst.ReturnScanCardToPool(_scanCard.gameObject);
        _scanCard.ResetScanCard();

        _scanCard = null;
    }
}
