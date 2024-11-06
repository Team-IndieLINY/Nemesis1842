using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    public static ScanManager Inst { get; private set; }
    public enum EScanType
    {
        CONDITION,
        LIVER,
        HEARTBEAT,
        FAIL
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

    [SerializeField]
    private Image _scanSelectorBackgroundImage;

    [SerializeField]
    private AlcoholController _alcoholController;

    [SerializeField]
    private HeartbeatScanner _heartbeatScanner;

    public EScanType CurrentScanType { get; set; }

    public ConditionScanData.EConditionType AnswerConditionType { get; set; }
    public LiverScanData.ELeavenType AnswerLeavenType { get; set; }
    public HeartbeatScanData.EHeartbeatType AnswerHeartbeatType { get; set; }
    
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

    public bool EvaluateScan(ScanData scanData)
    {
        if (scanData is ConditionScanData conditionScanData)
        {
            if (AnswerConditionType == conditionScanData.ConditionType)
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(31, 75, 9, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "성공";
                
                _alcoholController.ApplyScanResult(CurrentScanType);

                return true;
            }
            else
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(128, 23, 11, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "실패";
                _alcoholController.ApplyScanResult(EScanType.FAIL);
                return false;
            }
        }

        if (scanData is LiverScanData liverScanData)
        {
            if (AnswerLeavenType == liverScanData.LeavenType)
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(31, 75, 9, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "성공";
                _alcoholController.ApplyScanResult(CurrentScanType);
                return true;
            }
            else
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(128, 23, 11, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "실패";
                _alcoholController.ApplyScanResult(EScanType.FAIL);
                return false;
            }
        }

        if (scanData is HeartbeatScanData heartbeatScanData)
        {
            if (AnswerHeartbeatType == heartbeatScanData.HeartbeatType)
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(31, 75, 9, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "성공";
                _alcoholController.ApplyScanResult(CurrentScanType);
                return true;
            }
            else
            {
                _scanEvaluationTexts[(int)CurrentScanType].color = new Color32(128, 23, 11, 255);
                _scanEvaluationTexts[(int)CurrentScanType].text = "실패";
                _alcoholController.ApplyScanResult(EScanType.FAIL);
                return false;
            }
        }

        return false;
    }

    public void SetLiver(int squareLeavenCount, int circleLeavenCount, int starLeavenCount, int triangleLeaven)
    {
        _liver.SetLiver(squareLeavenCount, circleLeavenCount, starLeavenCount, triangleLeaven);
    }

    public void SetHeartbeatScanner()
    {
        _heartbeatScanner.SetHeartbeatScanner();
    }

    public void ResetStepScanManager()
    {
        foreach (var scanEvaluationText in _scanEvaluationTexts)
        {
            scanEvaluationText.text = "";
        }
        IsScanningDone = false;
        _liver.ResetLiver();
        _heartbeatScanner.ResetHeartbeatScanner();
    }

    public void FadeInScanSelectorBackground()
    {
        _scanSelectorBackgroundImage.DOFade(0f, 0.3f);
    }

    public void FadeOutScanSelectorBackground()
    {
        _scanSelectorBackgroundImage.DOColor(new Color32(255, 255, 255, 255), 0.3f);
    }
}
