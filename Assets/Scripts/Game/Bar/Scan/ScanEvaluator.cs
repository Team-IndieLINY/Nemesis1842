using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScanEvaluatorUI))]
public class ScanEvaluator : MonoBehaviour
{
    private ScanEvaluatorUI _scanEvaluatorUI;
    
    private string _answerConditionAttributeName;
    public bool IsSuccess { get; set; }

    private void Awake()
    {
        _scanEvaluatorUI = GetComponent<ScanEvaluatorUI>();
    }

    public void SetScanAnswer(int conditionValue)
    {
        if (conditionValue >= 0 && conditionValue < 33)
        {
            _answerConditionAttributeName = "나쁨";
        }
        else if (conditionValue >= 33 && conditionValue < 66)
        {
            _answerConditionAttributeName = "보통";
        }
        else if (conditionValue >= 66 && conditionValue <= 100)
        {
            _answerConditionAttributeName = "좋음";
        }
    }

    public void EvaluateScan(ScanCardSlot[] scanCardSlots)
    {
        IsSuccess = false;
        foreach (var scanCardSlot in scanCardSlots)
        {
            if (scanCardSlot.IsEmpty())
            {
                continue;
            }
            if (scanCardSlot.ScanCard.ScanData.AttributeName == _answerConditionAttributeName)
            {
                IsSuccess = true;
                break;
            }
        }
        
        _scanEvaluatorUI.UpdateScanEvaluatorUI(IsSuccess);
    }

    public void ResetScanEvaluator()
    {
        IsSuccess = false;
        _scanEvaluatorUI.ResetScanEvaluatorUI();
    }
}
