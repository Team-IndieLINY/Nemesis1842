using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiverGuideLineInfo : GuideLineInfo
{
    [SerializeField]
    private TextMeshProUGUI _leavenNameText;

    [SerializeField]
    private TextMeshProUGUI _leavenDescriptionText;
    
    [SerializeField]
    private Image _leavenImage;
    private void Awake()
    {
        LiverScanData liverScanData = _scanData as LiverScanData;

        _leavenNameText.text = liverScanData.LeavenName;
        _leavenDescriptionText.text = liverScanData.LeavenDescription;
        _leavenImage.sprite = liverScanData.LeavenSprite;
    }
}
