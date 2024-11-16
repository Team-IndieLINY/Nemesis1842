using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private Image _inActivateItemPanelImage;
    
    [SerializeField]
    private Image _inActivateAlcoholControllerPanelImage;

    [SerializeField]
    private Button _enterButton;

    [SerializeField]
    private int _utilitiesCost = 10;

    public int UtilitiesCost => _utilitiesCost;
    
    private int _maxAlcohol = 100;
    public int MaxAlcohol => _maxAlcohol;
    
    private int _minAlcohol = 0;
    public int MinAlcohol => _minAlcohol;
    
    private int _currentInputAlcohol = -1;
    public int CurrentInputAlcohol => _currentInputAlcohol;

    private int _currentAttempt = 0;
    public int CurrentAttempt => _currentAttempt;

    private int _sumOfUsingMachineCount = 0;
    public int SumOfUsingMachineCount => _sumOfUsingMachineCount;

    private bool _isAlcoholPhaseDone;
    public bool IsAlcoholPhaseDone => _isAlcoholPhaseDone;
    
    private int _attempt;
    private int _answerAlcohol;
    private bool _isUsedLiverScan;
    private Vector2 _originPosition;

    private Item _currentItem;
    public Item CurrentItem => _currentItem;

    private bool _isOverload;

    private void Awake()
    {
        _currentItem = null;
        InActivateAlcoholController();

        _isAlcoholPhaseDone = false;
        _enterButton.interactable = true;
    }

    public void SetAlcoholController(int answerAlcohol)
    {
        _answerAlcohol = answerAlcohol;
        _currentAttempt = 0;
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void ActivateAlcoholController()
    {
        _inActivateAlcoholControllerPanelImage.DOFade(0f, 0.5f)
            .OnKill(() =>
            {
                _inActivateAlcoholControllerPanelImage.raycastTarget = false;
            });
        
        _inActivateItemPanelImage.DOFade(0f, 0.5f)
            .OnKill(() =>
            {
                _inActivateItemPanelImage.raycastTarget = false;
                ApplyScanResult();
            });
    }
    
    public void InActivateAlcoholController()
    {
        _inActivateItemPanelImage.raycastTarget = true;
        _inActivateItemPanelImage.color = new Color32(0, 0, 0, 233);
        
        _inActivateAlcoholControllerPanelImage.raycastTarget = true;
        _inActivateAlcoholControllerPanelImage.color = new Color32(0, 0, 0, 233);
    }

    public void ApplyScanResult()
    {
        _alcoholControllerUI.UpdateScanBuffUI();
    }

    public void ResetStepAlcoholController()
    {
        _maxAlcohol = 100;
        _minAlcohol = 0;
        _currentInputAlcohol = -1;
        _currentAttempt = 0;
        _currentItem = null;
        _isUsedLiverScan = false;

        _enterButton.interactable = true;
        
        _alcoholControllerUI.UpdateAlcoholControllerUI();
        _alcoholControllerUI.ResetAlcoholControllerUI();
        _alcoholControllerUI.UpdateItemIconUI();

        InActivateAlcoholController();

        _isAlcoholPhaseDone = false;
    }

    public void ResetTurnAlcoholController()
    {
        _sumOfUsingMachineCount = 0;
    }

    public void EquipItem(Item item)
    {
        if (_currentItem != null && item != null)
        {
            _currentItem.IncreaseAmount(1);
        }
        _currentItem = item;

        _alcoholControllerUI.UpdateItemIconUI();
    }

    public void ResetItemSlot()
    {
        if (_currentItem == null)
        {
            return;
        }
        
        _currentItem.IncreaseAmount(1);
        _currentItem = null;
    }

    public void OnClickEnterButton()
    {
        AudioManager.Inst.PlaySFX("alcohol_machine_click");
        StartCoroutine(UpdateAlcoholController());
    }

    private IEnumerator UpdateAlcoholController()
    {
        if (_currentInputAlcohol == -1)
        {
            yield break;
        }
        
        _currentAttempt++;
        _sumOfUsingMachineCount++;

        if (_currentItem != null && _currentItem.ItemData.ItemType == ItemData.EItemType.Cooler)
        {
            _currentAttempt--;
            _currentItem = null;
        }

        // if (_currentAttempt >= 0)
        // {
        //     _originPosition = _alcoholControllerPanelTransform.position;
        //     Normalize();
        // }
        // else
        // {
        //     _sumOfOverloadCount++;
        //     OverDrive();
        // }

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
            _enterButton.interactable = false;
            // Normalize();
            
            _alcoholControllerUI.ChangeAnswerTextUI(true);
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());

            _isAlcoholPhaseDone = true;
        }
        else
        {
            _alcoholControllerUI.ChangeAnswerTextUI(false);
            
            if (_currentInputAlcohol > _minAlcohol && _currentInputAlcohol < _answerAlcohol)
            {
                _minAlcohol = _currentInputAlcohol;
            }
            else if (_currentInputAlcohol < _maxAlcohol && _currentInputAlcohol > _answerAlcohol)
            {
                _maxAlcohol = _currentInputAlcohol;
            }

            bool isHitLiverScan = false;
            
            if (ScanManager.Inst.CurrentScanData is LiverScanData)
            {
                if (_answerAlcohol % 10 == _currentInputAlcohol % 10 ||
                    _answerAlcohol / 10 == _currentInputAlcohol / 10)
                {
                    isHitLiverScan = true;
                }
            }
            
            _currentInputAlcohol = -1;

            AudioManager.Inst.PlaySFX("alcohol_machine_gauge");
            yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            yield return new WaitForSeconds(0.4f);
            
            if (ScanManager.Inst.CurrentScanData is ConditionScanData)
            {
                _minAlcohol = _minAlcohol + 3 > _answerAlcohol ? _answerAlcohol : _minAlcohol + 3;
                _maxAlcohol = _maxAlcohol - 3 < _answerAlcohol ? _answerAlcohol : _maxAlcohol - 3;
                
                yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            }

            if (ScanManager.Inst.CurrentScanData is LiverScanData && isHitLiverScan is true && _isUsedLiverScan is false)
            {
                _isUsedLiverScan = true;
                _minAlcohol = _minAlcohol + 10 > _answerAlcohol ? _answerAlcohol : _minAlcohol + 10;
                _maxAlcohol = _maxAlcohol - 10 < _answerAlcohol ? _answerAlcohol : _maxAlcohol - 10;
                
                yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            }
            
            if (ScanManager.Inst.CurrentScanData is HeartbeatScanData)
            {
                int randNum = Random.Range(1, 100);

                if (randNum > 30)
                {
                    yield break;
                }
                
                _maxAlcohol = _maxAlcohol - 20 < _answerAlcohol ? _answerAlcohol : _maxAlcohol - 20;
                
                yield return StartCoroutine(_alcoholControllerUI.UpdateAlcoholControllerUICoroutine());
            }
        }
    }

    public void OnClickCancelButton()
    {
        AudioManager.Inst.PlaySFX("alcohol_machine_click");
        _currentInputAlcohol = -1;
        _alcoholControllerUI.UpdateAlcoholControllerUI();
    }

    public void SetCurrentInputAlcohol(int keypadNum)
    {
        AudioManager.Inst.PlaySFX("alcohol_machine_click");
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
        _isOverload = false;
        _overloadImage.DOKill();
        _overloadImage.DOFade(0f, 0.2f);
        _alcoholControllerPanelTransform.DOKill();
        _alcoholControllerPanelTransform.DOMove(_originPosition, 0.2f);
    }
    private void OverDrive()
    {
        if (_isOverload is false)
        {
            _overloadImage.DOFade(0.3f, 1f).SetLoops(-1, LoopType.Yoyo);
            _isOverload = true;
        }

        _alcoholControllerPanelTransform
            .DOShakePosition(2f, _currentAttempt * -2f, _currentAttempt * -5, 40f, false, false)
            .SetLoops(-1, LoopType.Incremental);
    }
}