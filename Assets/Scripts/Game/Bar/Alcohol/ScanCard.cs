using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScanCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _attributeNameText;
    
    private ScanData _scanData;
    public ScanData ScanData => _scanData;
    
    private RectTransform _rectTransform;

    private bool _isClicked;
    
    //슬롯에 부착되었는가?
    public bool IsAttached { get; set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (IsAttached is true)
        {
            return;
        }
        
        if (_isClicked is false && Input.GetMouseButton(0))
        {
            _isClicked = true;
        
            _rectTransform.anchoredPosition = Input.mousePosition;
        }
        
        if (_isClicked is true && Input.GetMouseButton(0))
        {
            _rectTransform.anchoredPosition = Input.mousePosition;
        }

        if (_isClicked is true && Input.GetMouseButtonUp(0))
        {
            _isClicked = false;

            if (ScanCardStorage.Inst.IsHovered is true)
            {
                ScanCardStorage.Inst.InsertScanCard(this);
            }
            else
            {
                ScanCardPool.Inst.ReturnScanCardToPool(gameObject);
            }
        }
    }

    public void SetScanCard(ScanData scanData)
    {
        _scanData = scanData;
        
        _attributeNameText.text = scanData.AttributeName;
    }

    public void ResetScanCard()
    {
        IsAttached = false;
        _isClicked = false;
    }

    //타입이 같으면 true, 다르면 false
    public bool CompareScanCardType(ScanCard scanCard)
    {
        return _scanData.ScanType == scanCard._scanData.ScanType;
    }
}