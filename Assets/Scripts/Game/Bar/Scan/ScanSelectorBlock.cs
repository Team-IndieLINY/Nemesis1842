using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanSelectorBlock : MonoBehaviour
{
    [SerializeField]
    private ScanManager.EScanType _scanType;

    public void SetCurrentScanType()
    {
        ScanManager.Inst.CurrentScanType = _scanType;
    }
}
