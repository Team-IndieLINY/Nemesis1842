using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeartbeatScanData",menuName = "ScriptableObject/ScanData/HeartbeatScanData",order = Int32.MaxValue)]
public class HeartbeatScanData : ScanData
{
    public enum EHeartbeatType
    {
        NORMAL_TRIANGLE,
        TRANSFORMATION_TRIANGLE,
        CURVE_TRIANGLE,
        INVERSE_TRIANGLE,
        NORMAL_SQUARE,
        TRANSFORMATION_SQUARE,
        NARROW_SQUARE,
        INVERSE_SQUARE,
        ARRHYTHMIA,
        SQUARE_ARRHYTHMIA,
        CURVE_ARRHYTHMIA,
        LOVE_SICK
    }
    
    [SerializeField]
    private EHeartbeatType _heartbeatType;

    public EHeartbeatType HeartbeatType => _heartbeatType;
}
