using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlcoholControllerUI : MonoBehaviour
{
    [SerializeField]
    private AlcoholController _alcoholController;
    
    [SerializeField]
    private TextMeshProUGUI _maxAlcoholText;

    [SerializeField]
    private TextMeshProUGUI _minAlcoholText;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarBackgroundRectTransform;
    
    [SerializeField]
    private RectTransform _alcoholGaugeBarRectTransform;

    [SerializeField]
    private TextMeshProUGUI _guessAlcoholText;

    [SerializeField]
    private TextMeshProUGUI _attemptCountText;

    [SerializeField]
    private Image _itemSlotImage;

    [SerializeField]
    private List<Sprite> _scanIconSprites;

    [SerializeField]
    private Image _scanIconImage;
    
    private float _oneAlcoholPerGaugeLength;
    private bool _isChangedAlcoholRange;
    private bool _isChangedMaxGaugeBar;
    private bool _isChangedMinGaugeBar;

    private void Awake()
    {
        _oneAlcoholPerGaugeLength = _alcoholGaugeBarBackgroundRectTransform.sizeDelta.x * 0.01f;
    }
    
    public void UpdateAlcoholControllerUI()
    {
        UpdateGuessAlcoholUI();
        UpdateAttemptCountUI();
        
        _alcoholGaugeBarRectTransform.offsetMax = new Vector2(-(100 - _alcoholController.MaxAlcohol) * _oneAlcoholPerGaugeLength, 0);
        _alcoholGaugeBarRectTransform.offsetMin = new Vector2(_alcoholController.MinAlcohol * _oneAlcoholPerGaugeLength, 0);

        _maxAlcoholText.text = _alcoholController.MaxAlcohol.ToString();
        _minAlcoholText.text = _alcoholController.MinAlcohol.ToString();
    }

    public void UpdateItemSlotUI()
    {
        _itemSlotImage.sprite = _alcoholController.CurrentItem == null
            ? null
            : _alcoholController.CurrentItem.ItemData.ItemSprite;

        if (_itemSlotImage.sprite == null)
        {
            _itemSlotImage.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            _itemSlotImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void UpdateScanBuffUI()
    {
        _scanIconImage.sprite = _scanIconSprites[(int)_alcoholController.CurrentScanType];
        _scanIconImage.SetNativeSize();
        Vector2 size = _scanIconImage.rectTransform.sizeDelta;
        size.x *= 2;
        size.y *= 2;

        _scanIconImage.rectTransform.sizeDelta = size;
    }

    private void UpdateGuessAlcoholUI()
    {
        if (_alcoholController.CurrentInputAlcohol == -1)
        {
            _guessAlcoholText.text = "?";
        }
        else
        {
            _guessAlcoholText.text = _alcoholController.CurrentInputAlcohol.ToString();
        }
    }
    
    public IEnumerator UpdateAlcoholControllerUICoroutine()
    {
        UpdateGuessAlcoholUI();
        UpdateAttemptCountUI();
        
        yield return StartCoroutine(UpdateAlcoholRangeUICoroutine());
    }
    
    private IEnumerator UpdateAlcoholRangeUICoroutine()
    {
        //Update Range Text
        _isChangedAlcoholRange = true;
        StartCoroutine(ChangeAlcoholRangeTextCoroutine());
        
        //Update Gauge Bar
        _isChangedMaxGaugeBar = true;
        StartCoroutine(ChangeAlcoholMaxGaugeBarCoroutine());
        _isChangedMinGaugeBar = true;
        StartCoroutine(ChangeAlcoholMinGaugeBarCoroutine());

        yield return new WaitUntil(() =>
            _isChangedAlcoholRange == false && _isChangedMaxGaugeBar == false && _isChangedMinGaugeBar == false);
    }

    private IEnumerator ChangeAlcoholRangeTextCoroutine()
    {
        int maxAlcohol = Int32.Parse(_maxAlcoholText.text);
        int minAlcohol = Int32.Parse(_minAlcoholText.text);

        while (maxAlcohol > _alcoholController.MaxAlcohol || minAlcohol < _alcoholController.MinAlcohol)
        {
            if (maxAlcohol != _alcoholController.MaxAlcohol)
            {
                maxAlcohol--;
                _maxAlcoholText.text = maxAlcohol.ToString();
            }

            if(minAlcohol != _alcoholController.MinAlcohol)
            {
                minAlcohol++;
                _minAlcoholText.text = minAlcohol.ToString();
            }

            yield return new WaitForSeconds(0.02f);
        }

        _isChangedAlcoholRange = false;
    }
    
    private IEnumerator ChangeAlcoholMaxGaugeBarCoroutine()
    {
        Vector2 alcoholGaugeBarMaxOffset =
            new Vector2(-(100 - _alcoholController.MaxAlcohol) * _oneAlcoholPerGaugeLength, 0);

        while (_alcoholGaugeBarRectTransform.offsetMax.x > alcoholGaugeBarMaxOffset.x)
        {
            if (_alcoholGaugeBarRectTransform.offsetMax.x > alcoholGaugeBarMaxOffset.x)
            {
                _alcoholGaugeBarRectTransform.offsetMax =
                    new Vector2(_alcoholGaugeBarRectTransform.offsetMax.x - _oneAlcoholPerGaugeLength, 0);
            }

            yield return new WaitForSeconds(0.02f);
        }

        _isChangedMaxGaugeBar = false;
    }

    private IEnumerator ChangeAlcoholMinGaugeBarCoroutine()
    {
        Vector2 alcoholGaugeBarMinOffset =
            new Vector2(_alcoholController.MinAlcohol * _oneAlcoholPerGaugeLength, 0);

        while (_alcoholGaugeBarRectTransform.offsetMin.x < alcoholGaugeBarMinOffset.x)
        {
            if (_alcoholGaugeBarRectTransform.offsetMin.x < alcoholGaugeBarMinOffset.x)
            {
                _alcoholGaugeBarRectTransform.offsetMin =
                    new Vector2(_alcoholGaugeBarRectTransform.offsetMin.x + _oneAlcoholPerGaugeLength, 0);
            }

            yield return new WaitForSeconds(0.02f);
        }

        _isChangedMinGaugeBar = false;
    }
    
    private void UpdateAttemptCountUI()
    {
        _attemptCountText.text = _alcoholController.CurrentAttempt + " Turn";
    }
}
