using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScanManager : MonoBehaviour
{
    public static ScanManager Inst { get; private set; }
    public enum EScanType
    {
        CONDITION,
        LIVER,
        HEARTBEAT
    }

    [SerializeField]
    private RectTransform _scanGuideLineEnterPoint;

    [SerializeField]
    private RectTransform _scanGuideLineExitPoint;

    [SerializeField]
    private Transform _scannerEnterPoint;

    [SerializeField]
    private Transform _scannerExitPoint;
    
    [SerializeField]
    private Transform[] _scannerTransforms;

    [SerializeField]
    private RectTransform[] _scanGuideLineRectTransforms;

    [SerializeField]
    private Liver _liver;
    
    [SerializeField]
    protected TextMeshProUGUI[] _scanEvaluationTexts;

    public EScanType CurrentScanType { get; set; }

    public ConditionScanData.EConditionType AnswerConditionType { get; set; }
    public LiverScanData.ELeavenType AnswerLeavenType { get; set; }
    
    public bool IsScanningDone { get; private set; }

    private void Awake()
    {
        Inst = this;
        CurrentScanType = EScanType.CONDITION;

        foreach (var scannerRectTransform in _scannerTransforms)
        {
            scannerRectTransform.gameObject.SetActive(false);
            scannerRectTransform.position = _scannerExitPoint.position;
        }
        
        foreach (var scanGuideLineRectTransform in _scanGuideLineRectTransforms)
        {
            scanGuideLineRectTransform.anchoredPosition = _scanGuideLineExitPoint.anchoredPosition;
        }
    }

    public void EnterScanPhase()
    {
        _scannerTransforms[(int)CurrentScanType].DOMove(_scannerEnterPoint.position, 0.5f);
        _scannerTransforms[(int)CurrentScanType].gameObject.SetActive(true);
        if (CurrentScanType == EScanType.LIVER)
        {
            _liver.gameObject.SetActive(true);
        }
        _scanGuideLineRectTransforms[(int)CurrentScanType]
            .DOAnchorPos(_scanGuideLineEnterPoint.anchoredPosition, 0.5f);
    }
    
    public void ExitScanPhase()
    {
        _scannerTransforms[(int)CurrentScanType].DOMove(_scannerExitPoint.position, 0.5f);
        _scanGuideLineRectTransforms[(int)CurrentScanType]
            .DOAnchorPos(_scanGuideLineExitPoint.anchoredPosition, 0.5f)
            .OnKill(() =>
            {
                _scannerTransforms[(int)CurrentScanType].gameObject.SetActive(false);
                IsScanningDone = true;
            });
    }

    public void EvaluateScan(ScanData scanData)
    {
        if (scanData is ConditionScanData conditionScanData)
        {
            if (AnswerConditionType == conditionScanData.ConditionType)
            {
                _scanEvaluationTexts[(int)CurrentScanType].text = "SUCCESS";
            }
            else
            {
                _scanEvaluationTexts[(int)CurrentScanType].text = "FAIL";
            }
        }
        else if (scanData is LiverScanData liverScanData)
        {
            if (AnswerLeavenType == liverScanData.LeavenType)
            {
                _scanEvaluationTexts[(int)CurrentScanType].text = "SUCCESS";
            }
            else
            {
                _scanEvaluationTexts[(int)CurrentScanType].text = "FAIL";
            }
        }
    }

    public void SetLiver(int squareLeavenCount, int circleLeavenCount, int starLeavenCount)
    {
        _liver.SetLiver(squareLeavenCount, circleLeavenCount, starLeavenCount);
    }

    public void ResetScanManager()
    {
        foreach (var scanEvaluationText in _scanEvaluationTexts)
        {
            scanEvaluationText.text = "";
        }
        IsScanningDone = false;
        _liver.ResetLiver();
    }
}
