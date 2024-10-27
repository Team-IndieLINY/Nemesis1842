using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlcoholController : MonoBehaviour
{
    [SerializeField]
    private BarGameManager _barGameManager;
    
    [SerializeField]
    private AlcoholControllerUI _alcoholControllerUI;

    [SerializeField]
    private Transform _alcoholControllerPanelTransform;

    [SerializeField]
    private Image _overloadImage;
    
    private int _maxAlcohol = 100;
    public int MaxAlcohol => _maxAlcohol;
    
    private int _minAlcohol = 0;
    public int MinAlcohol => _minAlcohol;
    
    private int _currentInputAlcohol = -1;
    public int CurrentInputAlcohol => _currentInputAlcohol;

    private int _currentAttempt = 0;
    public int CurrentAttempt => _currentAttempt;

    private ScanManager.EScanType _currentScanType;
    public ScanManager.EScanType CurrentScanType => _currentScanType;

    private int _sumOfOverloadCount = 0;
    public int SumOfOverloadCount => _sumOfOverloadCount;
    
    private int _attempt;
    private int _answerAlcohol;
    private bool _isUsedLiverScan;
    private Vector2 _originPosition;

    private Item _currentItem;
    public Item CurrentItem => _currentItem;

    private void Awake()
    {
        _currentItem = null;
    }

    public void SetAlcoholController(int answerAlcohol, int attempt)
    {
        _answerAlcohol = answerAlcohol;
        _attempt = attempt;
        _currentAttempt = _attempt;
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void ApplyScanResult(ScanManager.EScanType scanType)
    {
        _currentScanType = scanType;
        _alcoholControllerUI.UpdateScanBuffUI();
    }

    public void ResetStepAlcoholController()
    {
        _maxAlcohol = 100;
        _minAlcohol = 0;
        _currentInputAlcohol = -1;
        _currentItem = null;
        _isUsedLiverScan = false;
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
        _alcoholControllerUI.UpdateItemSlotUI();
    }

    public void ResetTurnAlcoholController()
    {
        _sumOfOverloadCount = 0;
    }

    public void EquipItem(Item item)
    {
        if (_currentItem != null)
        {
            _currentItem.IncreaseAmount(1);
        }
        _currentItem = item;
        _alcoholControllerUI.UpdateItemSlotUI();
    }

    public void ResetItemSlot()
    {
        if (_currentItem == null)
        {
            return;
        }
        
        _currentItem.IncreaseAmount(1);
        _currentItem = null;
        
        _alcoholControllerUI.UpdateItemSlotUI();
    }

    public void OnClickEnterButton()
    {
        StartCoroutine(UpdateAlcoholController());
    }

    private IEnumerator UpdateAlcoholController()
    {
        if (_currentInputAlcohol == -1)
        {
            yield break;
        }
        
        _currentAttempt--;

        if (_currentItem != null && _currentItem.ItemData.ItemType == ItemData.EItemType.Cooler)
        {
            _currentAttempt += 2;
            _currentItem = null;
        }

        if (_currentAttempt >= 0)
        {
            _originPosition = _alcoholControllerPanelTransform.position;
            Normalize();
        }
        else
        {
            _sumOfOverloadCount++;
            OverDrive();
        }

        int _increasedMaxAmount = 0;
        int _increasedMinAmount = 0;
        if (_currentItem != null && _currentItem.ItemData.ItemType == ItemData.EItemType.AIEnhancer) 
        {
            _increasedMaxAmount = 3;
            _increasedMinAmount = 3;
            _currentItem = null;
        }
        
        if (_currentInputAlcohol <= _answerAlcohol + _increasedMaxAmount && _currentInputAlcohol >= _answerAlcohol - _increasedMinAmount)
        {
            Normalize();
            StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            _barGameManager.OnClickEnterCutSceneButton();
        }
        else
        {
            if (_currentInputAlcohol > _minAlcohol && _currentInputAlcohol < _answerAlcohol)
            {
                _minAlcohol = _currentInputAlcohol;
            }
            else if (_currentInputAlcohol < _maxAlcohol && _currentInputAlcohol > _answerAlcohol)
            {
                _maxAlcohol = _currentInputAlcohol;
            }

            bool isHitLiverScan = false;
            
            if (_currentScanType == ScanManager.EScanType.LIVER)
            {
                if (_answerAlcohol % 10 == _currentInputAlcohol % 10 ||
                    _answerAlcohol / 10 == _currentInputAlcohol / 10)
                {
                    isHitLiverScan = true;
                }
            }
            
            _currentInputAlcohol = -1;
            

            _alcoholControllerUI.UpdateItemSlotUI();
            yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            yield return new WaitForSeconds(0.4f);
            
            if (_currentScanType == ScanManager.EScanType.CONDITION)
            {
                _minAlcohol = _minAlcohol + 3 > _answerAlcohol ? _answerAlcohol : _minAlcohol + 3;
                _maxAlcohol = _maxAlcohol - 3 < _answerAlcohol ? _answerAlcohol : _maxAlcohol - 3;
                
                yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            }

            if (_currentScanType == ScanManager.EScanType.LIVER && isHitLiverScan is true && _isUsedLiverScan is false)
            {
                _isUsedLiverScan = true;
                _minAlcohol = _minAlcohol + 10 > _answerAlcohol ? _answerAlcohol : _minAlcohol + 10;
                _maxAlcohol = _maxAlcohol - 10 < _answerAlcohol ? _answerAlcohol : _maxAlcohol - 10;
                
                yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            }
        }
    }

    public void OnClickCancelButton()
    {
        _currentInputAlcohol = -1;
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void SetCurrentInputAlcohol(int keypadNum)
    {
        if (_currentInputAlcohol < 0)
        {
            _currentInputAlcohol = keypadNum;
        }
        else if (_currentInputAlcohol < 10)
        {
            _currentInputAlcohol *= 10;
            _currentInputAlcohol += keypadNum;
        }
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    private void Normalize()
    {
        _overloadImage.DOKill();
        _overloadImage.DOFade(0f, 0.2f);
        _alcoholControllerPanelTransform.DOKill();
        _alcoholControllerPanelTransform.DOMove(_originPosition, 0.2f);
    }
    private void OverDrive()
    {
        _overloadImage.DOFade(0.3f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        _alcoholControllerPanelTransform.DOShakePosition(2f, _currentAttempt * -2f, _currentAttempt * -5, 40f, false, false).SetLoops(-1, LoopType.Incremental);
    }
}