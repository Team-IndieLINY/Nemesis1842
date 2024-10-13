using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GuideLineInfo : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    protected ScanData _scanData;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(WaitEvaluationScanCoroutine());
    }

    private IEnumerator WaitEvaluationScanCoroutine()
    {
        ScanManager.Inst.EvaluateScan(_scanData);

        yield return new WaitForSeconds(1f);
        
        ScanManager.Inst.ExitScanPhase();
    }
}