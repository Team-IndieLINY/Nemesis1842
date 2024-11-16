using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LockUI : MonoBehaviour, IPopUpable
{
    [SerializeField]
    private Lock _lock;

    [SerializeField]
    private TextMeshProUGUI[] _inputNumberTexts = new TextMeshProUGUI[4];

    [SerializeField]
    private Image[] _lines;

    [SerializeField]
    private Button[] _keypadButtons;

    [SerializeField]
    private Transform _lockTransform;

    [SerializeField]
    private Button _enterButton;
    
    [SerializeField]
    private Button _resetButton;
    
    [SerializeField]
    private Sprite _clickedKeypadSprite;
    
    [SerializeField]
    private Sprite _unClickedKeypadSprite;

    [SerializeField]
    private RectTransform _lockHeadRectTransform;
    
    [SerializeField]
    private RectTransform _unlockedLockHeadPointRectTransform;

    private Image[] _keypadImages;
    
    private void Awake()
    {
        _enterButton.interactable = false;

        _keypadImages = new Image[_keypadButtons.Length];

        for (int i = 0; i < _keypadImages.Length; i++)
        {
            _keypadImages[i] = _keypadButtons[i].GetComponent<Image>();
        }

        for (int i = 0; i < _inputNumberTexts.Length; i++)
        {
            _inputNumberTexts[i].text = "";
            _lines[i].color = new Color32(255, 255, 255, 0);
        }
        
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        PlayerController.RestrictMovement();
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        PlayerController.AllowMovement();
        gameObject.SetActive(false);
    }

    public void UpdateLockUI()
    {
        if (_lock.CurrentNumberString.Length == 0)
        {
            for (int i = 0; i < _keypadImages.Length; i++)
            {
                _keypadImages[i].sprite = _unClickedKeypadSprite;

                _keypadImages[i].raycastTarget = true;
                _keypadButtons[i].interactable = true;
            }
        }
        
        _enterButton.interactable = _lock.CurrentNumberString.Length == _inputNumberTexts.Length;

        for (int i = 0; i < _inputNumberTexts.Length; i++)
        {
            if (i >= _lock.CurrentNumberString.Length)
            {
                _inputNumberTexts[i].text = "";
                _lines[i].color = new Color32(255, 255, 255, 0);
                continue;
            }
            
            _inputNumberTexts[i].text = _lock.CurrentNumberString[i].ToString();
            _lines[i].color = new Color32(255, 255, 255, 255);
        }

        if (_lock.CurrentNumberString.Length > 0)
        {
            int lastNumber = Int32.Parse(_lock.CurrentNumberString[^1].ToString());

            if (lastNumber == 0)
            {
                lastNumber = 10;
            }
            
            _keypadButtons[lastNumber - 1].interactable = false;
            _keypadImages[lastNumber - 1].raycastTarget = false;
            _keypadImages[lastNumber - 1].sprite = _clickedKeypadSprite;
        }
    }

    public void CorrectPassword()
    {
        _lockHeadRectTransform.DOAnchorPos(_unlockedLockHeadPointRectTransform.anchoredPosition, 0.5f);
        _enterButton.interactable = false;
        _resetButton.interactable = false;
    }

    public void InCorrectPassword()
    {
        _lockTransform.DOKill();
        _lockTransform.DOShakePosition(0.5f, 15f, 20, 40f, false, false);
    }
}
