using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LiverScanData",menuName = "ScriptableObject/ScanData/LiverScanData",order = Int32.MaxValue)]
public class LiverScanData : ScanData
{
    public enum ELeavenType
    {
        SQUARE,
        CIRCLE,
        STAR
    }

    [SerializeField]
    private ELeavenType _leavenType;

    public ELeavenType LeavenType => _leavenType;

    [SerializeField]
    private string _leavenName;
    
    public string LeavenName => _leavenName;
    
    [SerializeField]
    private Sprite _leavenSprite;
    
    public Sprite LeavenSprite => _leavenSprite;

    [SerializeField]
    private string _leavenDescription;

    public string LeavenDescription => _leavenDescription;
}