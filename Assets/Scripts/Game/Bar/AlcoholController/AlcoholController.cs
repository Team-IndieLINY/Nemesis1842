using System;
using System.Collections;
using System.Collections.Generic;
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
    private int _maxAnswerAlcohol;
    private int _minAnswerAlcohol;

    private Item _currentItem;
    public Item CurrentItem => _currentItem;

    private void Awake()
    {
        _currentItem = null;
    }

    public void SetAlcoholController(int maxAnswerAlcohol, int minAnswerAlcohol, int attempt)
    {
        _maxAnswerAlcohol = maxAnswerAlcohol;
        _minAnswerAlcohol = minAnswerAlcohol;
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
        
        if (_currentInputAlcohol <= _maxAnswerAlcohol + _increasedMaxAmount && _currentInputAlcohol >= _minAnswerAlcohol - _increasedMinAmount)
        {
            StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            _barGameManager.OnClickEnterCutSceneButton();
        }
        else
        {
            if (_currentInputAlcohol > _minAlcohol && _currentInputAlcohol < _minAnswerAlcohol)
            {
                _minAlcohol = _currentInputAlcohol;
            }
            else if(_currentInputAlcohol < _maxAlcohol && _currentInputAlcohol > _maxAnswerAlcohol)
            {
                _maxAlcohol = _currentInputAlcohol;
            }
            
            _currentInputAlcohol = -1;
            

            _alcoholControllerUI.UpdateItemSlotUI();
            yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            yield return new WaitForSeconds(0.4f);
            
            if (_currentScanType == ScanManager.EScanType.CONDITION)
            {
                _minAlcohol = _minAlcohol + 3 > _minAnswerAlcohol ? _minAnswerAlcohol : _minAlcohol + 3;
                _maxAlcohol = _maxAlcohol - 3 < _maxAnswerAlcohol ? _maxAnswerAlcohol : _maxAlcohol - 3;
                
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
        //정상화 연출
        Debug.Log("정상화");
    }
    private void OverDrive()
    {
        //과부하 연출
        Debug.Log("과부하");
    }
}