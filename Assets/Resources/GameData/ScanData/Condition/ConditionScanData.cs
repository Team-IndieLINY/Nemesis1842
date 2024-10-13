using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionScanData",menuName = "ScriptableObject/ScanData/ConditionScanData",order = Int32.MaxValue)]
public class ConditionScanData : ScanData
{
    public enum EConditionType
    {
        BAD,
        NORMAL,
        GOOD
    }

    [SerializeField]
    private EConditionType _conditionType;

    public EConditionType ConditionType => _conditionType;

    [SerializeField]
    private string _conditionName;
    
    public string ConditionName => _conditionName;
}