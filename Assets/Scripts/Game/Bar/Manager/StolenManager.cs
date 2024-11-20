using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StolenManager : MonoBehaviour
{
    public static StolenManager Inst { get; private set; }
    
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
    private TextMeshProUGUI _itemNameTagText;

    [SerializeField]
    private GameObject _itemNameTagGO;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private GameObject _newItemRedDotGO;
    
    private bool _isStolenDone;
    public bool IsStolenDone => _isStolenDone;

    private List<BelongingData> _belongingDatas;

    private GameObject _currentBelonging;

    private CanvasGroup _stopStealingButtonCanvasGroup;
    
    private void Awake()
    {
        Inst = this;
        _newItemRedDotGO.SetActive(PlayerManager.Instance().IsNewItemDotActivated);
        
        _stopStealingButtonCanvasGroup = _stopStealingButton.GetComponent<CanvasGroup>();
        _belongingDatas = Resources.LoadAll<BelongingData>("GameData/BelongingData").ToList();
        ResetTurnStolenManager();

        _stopStealingButton.interactable = false;
        _stopStealingButtonCanvasGroup.alpha = 0;
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

        _stolenSelectorCanvasGroup.DOKill();
        _stolenSelectorCanvasGroup.DOFade(0f, 0.2f);
        
        RectTransform currentBelongingRectTransform = _currentBelonging.transform as RectTransform;
        
        currentBelongingRectTransform.DOAnchorPos(_belongingShownPointRectTransform.anchoredPosition, 0.5f)
            .OnKill(() =>
            {
                _inventory.OpenStealInventory();
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
        
        _inventory.CloseStealInventory();
        _stopStealingButtonCanvasGroup.DOFade(0f, 0.2f)
            .OnKill(() =>
            {
                currentBelongingRectTransform.DOAnchorPos(_belongingSpawnPointRectTransform.anchoredPosition, 0.5f)
                    .OnKill(() =>
                    {
                        _stolenSelectorCanvasGroup.DOFade(1f, 0.2f);
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

    public void InActivateStealableItems()
    {
        _currentBelonging.transform.GetChild(0).GetComponent<Belonging>().InActivateStealableItems();
    }
    
    public void ActivateStealableItems()
    {
        _currentBelonging.transform.GetChild(0).GetComponent<Belonging>().ActivateStealableItems();
    }

    public void ShowStealableItemNameTag(string itemName)
    {
        _itemNameTagText.text = itemName;
        _itemNameTagGO.SetActive(true);
    }

    public void UpdateStealableItemNameTagPosition(Vector2 mouseWorldPosition)
    {
        Vector2 mouseRightUpPosition = new Vector2(mouseWorldPosition.x + 0.8f, mouseWorldPosition.y + 0.4f);
        _itemNameTagGO.transform.position = mouseRightUpPosition;
    }

    public void HideStealableItemNameTag()
    {
        _itemNameTagGO.SetActive(false);
    }

    public void StealMoney(int money)
    {
        _player.EarnMoney(money);
    }

    public void StealInformationItem(InformationItemData informationItemData)
    {
        _newItemRedDotGO.SetActive(true);
        PlayerManager.Instance().IsNewItemDotActivated = true;
        _inventory.AddItem(informationItemData);
        InventoryManager.Instance().TodayAquireInformationItemDatas.Add(informationItemData);
    }
}