using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    private TextMeshProUGUI _scanBuffDescriptionText;

    [SerializeField]
    private List<Sprite> _scanIconSprites;

    [SerializeField]
    private Image _scanIconImage;

    [SerializeField]
    private Image _itemIconImage;

    [SerializeField]
    private GameObject _cocktailIconImage;

    [SerializeField]
    private Sprite[] _itemIconSprites;
    
    private float _oneAlcoholPerGaugeLength;
    private bool _isChangedAlcoholRange;
    private bool _isChangedMaxGaugeBar;
    private bool _isChangedMinGaugeBar;

    private void Awake()
    {
        var transform1 = _scanIconImage.transform;
        transform1.localScale = new Vector3(transform1.localScale.x, 0, 1);
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

    public void UpdateScanBuffUI()
    {
        if (ScanManager.Inst.CurrentScanData == null)
        {
            _scanIconImage.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            _scanIconImage.color = new Color32(255, 255, 255, 255);
        }
        
        _scanIconImage.sprite = _scanIconSprites[(int)ScanManager.Inst.CurrentScanType];
        _scanIconImage.SetNativeSize();

        _scanIconImage.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.2f);

        if (ScanManager.Inst.CurrentScanData == null)
        {
            StartCoroutine(TypeScanBuffDescription(""));
        }
        else
        {
            StartCoroutine(TypeScanBuffDescription(ScanManager.Inst.CurrentScanData.ScanBuffDescription));
        }
        
    }

    private IEnumerator TypeScanBuffDescription(string str)
    {
        _scanBuffDescriptionText.text = "";

        foreach (var letter in str.ToCharArray())
        {
            _scanBuffDescriptionText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void ChangeAnswerTextUI(bool isAnswer)
    {
        if (isAnswer is true)
        {
            _guessAlcoholText.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            _guessAlcoholText.color = new Color32(255, 0, 0, 255);
            _guessAlcoholText.rectTransform.DOShakeAnchorPos(0.4f, 10f, 50, 40, false, false)
                .OnKill(() =>
                {
                    _guessAlcoholText.color = new Color32(250, 182, 47, 255);
                });
        }
    }

    private void UpdateGuessAlcoholUI()
    {
        if (_alcoholController.CurrentInputAlcohol == -1)
        {
            _guessAlcoholText.gameObject.SetActive(false);
            _cocktailIconImage.SetActive(true);
        }
        else
        {
            _cocktailIconImage.SetActive(false);
            _guessAlcoholText.gameObject.SetActive(true);
            _guessAlcoholText.text = _alcoholController.CurrentInputAlcohol.ToString();
        }
    }
    
    public IEnumerator UpdateAlcoholControllerUICoroutine()
    {
        UpdateGuessAlcoholUI();
        
        yield return StartCoroutine(UpdateAlcoholRangeUICoroutine());
        yield return StartCoroutine(UpdateAttemptCountUICoroutine());
    }

    public void ResetAlcoholControllerUI()
    {
        _guessAlcoholText.color = new Color32(250, 182, 47, 255);
        _scanIconImage.color = new Color32(255, 255, 255, 0);
        _scanBuffDescriptionText.text = "";
        
        var transform1 = _scanIconImage.transform;
        transform1.localScale = new Vector3(transform1.localScale.x, 0, 1);
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
        _attemptCountText.text = (_alcoholController.CurrentAttempt * _alcoholController.UtilitiesCost).ToString();
    }
    
    private IEnumerator UpdateAttemptCountUICoroutine()
    {
        int currentUtilitiesCost = Int32.Parse(_attemptCountText.text);
        
        while (currentUtilitiesCost < _alcoholController.CurrentAttempt * _alcoholController.UtilitiesCost)
        {
            currentUtilitiesCost++;

            _attemptCountText.text = currentUtilitiesCost.ToString();

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void UpdateItemIconUI()
    {
        _itemIconImage.DOKill();
        
        if (_alcoholController.CurrentItem == null)
        {
            _itemIconImage.color = new Color32(255, 255, 255, 0);
            return;
        }
        
        _itemIconImage.sprite = _itemIconSprites[(int)_alcoholController.CurrentItem.ItemData.ItemType];
        
        _itemIconImage.color = new Color32(255, 255, 255, 255);
        _itemIconImage.DOFade(0f, 0.7f).SetLoops(-1, LoopType.Yoyo);
    }
}
