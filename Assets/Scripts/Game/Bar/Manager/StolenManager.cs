using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StolenManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _stolenSelectorCanvasGroup;

    [SerializeField]
    private Image _stealPanelBackground;

    [SerializeField]
    private Button _wakeUpButton;
    
    [SerializeField]
    private Button _stealButton;

    [SerializeField]
    private Button _stopStealingButton;

    [SerializeField]
    private RectTransform _stealPanelRectTransform;
    
    [SerializeField]
    private RectTransform _belongingSpawnPointRectTransform;
    
    [SerializeField]
    private RectTransform _belongingShownPointRectTransform;

    [SerializeField]
    private Image _inventoryImage;
    
    private bool _isStolenDone;
    public bool IsStolenDone => _isStolenDone;

    private List<BelongingData> _belongingDatas;

    private GameObject _currentBelonging;

    private CanvasGroup _stopStealingButtonCanvasGroup;
    private CanvasGroup _inventoryImageCanvasGroup;
    
    private void Awake()
    {
        _stopStealingButtonCanvasGroup = _stopStealingButton.GetComponent<CanvasGroup>();
        _inventoryImageCanvasGroup = _inventoryImage.GetComponent<CanvasGroup>();
        _belongingDatas = Resources.LoadAll<BelongingData>("GameData/BelongingData").ToList();
        ResetTurnStolenManager();

        _stopStealingButton.interactable = false;
        _stopStealingButtonCanvasGroup.alpha = 0;

        _inventoryImage.raycastTarget = false;
    }

    public void SetStolenManager(string guestCode)
    {
        foreach (var belongingData in _belongingDatas)
        {
            if (belongingData.GuestCode == guestCode)
            {
                var position = _belongingSpawnPointRectTransform.position;
                
                _currentBelonging = Instantiate(belongingData.BelongingPrefab,
                    new Vector3(position.x,
                        position.y, 0),
                    Quaternion.identity,
                    _stealPanelRectTransform);
                
                break;
            }
        }
    }

    public void ShowStolenSelector()
    {
        _stealPanelBackground.DOFade(0.7f, 0.3f);
        _stolenSelectorCanvasGroup.gameObject.SetActive(true);
        _stolenSelectorCanvasGroup.DOFade(1f, 0.3f);
    }

    public void OnClickWakeUpButton()
    {
        _isStolenDone = true;
        
        _stealPanelBackground.DOFade(0f, 0.3f);
        _stolenSelectorCanvasGroup.DOFade(0f, 0.3f)
            .OnKill(()=>
        {
            _stolenSelectorCanvasGroup.gameObject.SetActive(false);
        });
    }
    
    public void OnClickStealButton()
    {
        _wakeUpButton.interactable = false;
        RectTransform currentBelongingRectTransform = _currentBelonging.transform as RectTransform;

        _inventoryImageCanvasGroup.DOFade(1f, 0.3f)
            .OnKill(() =>
            {
                _inventoryImage.raycastTarget = true;
            });
        
        currentBelongingRectTransform.DOAnchorPos(_belongingShownPointRectTransform.anchoredPosition, 0.5f)
            .OnKill(() =>
            {
                _stopStealingButtonCanvasGroup.DOFade(1f, 0.2f)
                    .OnKill(() =>
                    {
                        _stopStealingButton.interactable = true;
                    });
            });
    }

    public void OnClickStopStealingButton()
    {
        _stopStealingButton.interactable = false;
        
        RectTransform currentBelongingRectTransform = _currentBelonging.transform as RectTransform;

        _inventoryImage.raycastTarget = false;
        _inventoryImageCanvasGroup.DOFade(0f, 0.3f);
        
        _stopStealingButtonCanvasGroup.DOFade(0f, 0.2f)
            .OnKill(() =>
            {
                currentBelongingRectTransform.DOAnchorPos(_belongingSpawnPointRectTransform.anchoredPosition, 0.5f)
                    .OnKill(() =>
                    {
                        _wakeUpButton.interactable = true;
                        _stealButton.interactable = true;
                    });
            });
    }

    public void ResetTurnStolenManager()
    {
        _wakeUpButton.interactable = true;
        _stealButton.interactable = true;
        _stolenSelectorCanvasGroup.gameObject.SetActive(false);
        _isStolenDone = false;

        if (_currentBelonging != null)
        {
            Destroy(_currentBelonging);
            _currentBelonging = null;
        }
    }
}
