using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ScanData : ScriptableObject
{
    [SerializeField]
    protected string _scanBuffDescription;

    public string ScanBuffDescription => _scanBuffDescription;
}