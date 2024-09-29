using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScanData",menuName = "ScriptableObject/ScanData",order = Int32.MaxValue)]
public class ScanData : ScriptableObject
{
    public enum EScanType
    {
        Condition,
        Liver,
        Heartbeat
    }
    [SerializeField]
    private string _attributeName;

    public string AttributeName => _attributeName;

    [SerializeField]
    private int _maxAlcohol;

    public int MaxAlcohol => _maxAlcohol;
    
    [SerializeField]
    private int _minAlcohol;

    public int MinAlcohol => _minAlcohol;

    [SerializeField]
    private EScanType _scanType;

    public EScanType ScanType => _scanType;

    [SerializeField]
    private string _description;

    public string Description => _description;
}