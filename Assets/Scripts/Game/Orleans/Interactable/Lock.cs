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
    
    private int _maxAnswerNumberStringLength;
    
    private string _currentNumberString = "";
    public string CurrentNumberString => _currentNumberString;

    private void Awake()
    {
        _maxAnswerNumberStringLength = answerNumberString.Length;
        _objectIconImage.fillAmount = 0;
        _objectNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public override void Interact()
    {
        PopUpUIManager.Inst.OpenUI(_lockUI);
    }

    public void OnClickKeyPadButton(int number)
    {
        if (_currentNumberString.Length >= _maxAnswerNumberStringLength)
        {
            return;
        }
        
        _currentNumberString += number;
        
        _lockUI.UpdateLockUI();
    }

    public void OnClickEnterButton()
    {
        if (_currentNumberString == answerNumberString)
        {
            _lockUI.CorrectPassword();
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
