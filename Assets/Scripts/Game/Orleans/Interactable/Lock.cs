using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : InteractableObject
{
    [SerializeField]
    private string answerNumberString;

    [SerializeField]
    private LockUI _lockUI;

    [SerializeField]
    private List<string> _cantInteractScripts;

    [SerializeField]
    private Transform _playerTransform;
    
    private int _maxAnswerNumberStringLength;
    
    private string _currentNumberString = "";
    public string CurrentNumberString => _currentNumberString;

    private bool _isInteractFirstTime = true;
    public bool IsInteractFirstTime => _isInteractFirstTime;

    private void Awake()
    {
        _maxAnswerNumberStringLength = answerNumberString.Length;
        _objectIconImage.fillAmount = 0;
        _objectNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public override void Interact()
    {
        AudioManager.Inst.PlaySFX("interact_lock");
        if (DayManager.Instance.Day == 3 && DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
        {
            if (_isInteractFirstTime == true)
            {
                StartCoroutine(LockCutSceneManager.Inst.StartCutScene());
                _isInteractFirstTime = false;
            }
            else
            {
                PopUpUIManager.Inst.OpenUI(_lockUI);
            }
        }
        else
        {
            BarOutsideDialougeManager.Inst.StartDialogueByString(_playerTransform.position, _cantInteractScripts);
        }
    }

    public void OnClickKeyPadButton(int number)
    {
        if (_currentNumberString.Length >= _maxAnswerNumberStringLength)
        {
            return;
        }
        
        _currentNumberString += number;
        AudioManager.Inst.PlaySFX("open_lock_button");
        _lockUI.UpdateLockUI();
    }

    public void OnClickEnterButton()
    {
        if (_currentNumberString == answerNumberString)
        {
            _lockUI.CorrectPassword();
            LockCutSceneManager.Inst.StopTimer();
            
        }
        else
        {
            _lockUI.InCorrectPassword();
            OnClickResetButton();
        }
    }
    
    public void OnClickResetButton()
    {
        _currentNumberString = "";
        _lockUI.UpdateLockUI();
    }
}
