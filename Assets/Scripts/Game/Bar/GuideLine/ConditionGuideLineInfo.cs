using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConditionGuideLineInfo : GuideLineInfo
{
    [SerializeField]
    private TextMeshProUGUI _conditionNameText;
    
    private void Awake()
    {
        ConditionScanData conditionScanData = _scanData as ConditionScanData;

        _conditionNameText.text = conditionScanData.ConditionName;
    }
}
