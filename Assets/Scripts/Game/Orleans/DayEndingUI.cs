using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DayEndingUI : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _dayText;

    [SerializeField]
    private TextMeshProUGUI _lastDayText;

    [SerializeField]
    private TextMeshProUGUI _currentMoneyText;

    [SerializeField]
    private TextMeshProUGUI _lastRentText;
    
    [SerializeField]
    private TextMeshProUGUI _usingAIEnhancerItemCountText;
    
    [SerializeField]
    private TextMeshProUGUI _usingCoolerItemCountText;
    
    [SerializeField]
    private Image[] _aquireItemSlots;

    [SerializeField]
    private Image _pressAnyKeyImage;

    private CanvasGroup _canvasGroup;
    private List<Coroutine> _updateDayEndingUICoroutine = new();
    private int _doneCoroutineCount = 0;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown && _canvasGroup.blocksRaycasts == true && _doneCoroutineCount > 0)
        {
            foreach (var coroutine in _updateDayEndingUICoroutine)
            {
                if (coroutine == null)
                {
                    continue;
                }
                StopCoroutine(coroutine);
            }
            SkipTyping();
            _doneCoroutineCount = 0;
            _updateDayEndingUICoroutine.Clear();
        }
        else if (Input.anyKeyDown && _canvasGroup.blocksRaycasts == true && _doneCoroutineCount == 0)
        {
            CloseDayEndingUI();
        }
    }

    public void OpenDayEndingUI()
    {
        _dayText.text = DayManager.Instance.Day.ToString();
        _lastDayText.text = "월세 지급일까지 " + (3 - DayManager.Instance.Day) + "일 남음!";
        _usingAIEnhancerItemCountText.text = "x" + PlayerManager.Instance().AIEnhancerItemUsingCount;
        _usingCoolerItemCountText.text = "x" + PlayerManager.Instance().CoolerItemUsingCount;
        
        for (int i = 0; i < _aquireItemSlots.Length; i++)
        {
            if (i >= InventoryManager.Instance().TodayAquireInformationItemDatas.Count)
            {
                _aquireItemSlots[i].color = new Color32(255, 255, 255, 0);
            }
            else
            {
                _aquireItemSlots[i].sprite = InventoryManager.Instance().TodayAquireInformationItemDatas[i].ItemSprite;
                _aquireItemSlots[i].SetNativeSize();
                
                _aquireItemSlots[i].color = new Color32(255, 255, 255, 255);
            }
        }

        _pressAnyKeyImage.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo);
        _canvasGroup.DOFade(1f, 2f)
            .OnKill(() =>
            {
                _updateDayEndingUICoroutine.Add(StartCoroutine(TypeEndingUI(0, PlayerManager.Instance().Money,
                    _currentMoneyText)));
                _updateDayEndingUICoroutine.Add(StartCoroutine(TypeEndingUI(0,
                    Mathf.Clamp(DayManager.Instance.MonthlyRent - PlayerManager.Instance().Money, 0, Int32.MaxValue),
                    _lastRentText)));
                
                _canvasGroup.blocksRaycasts = true;
            });
    }
    public void CloseDayEndingUI()
    {
        _pressAnyKeyImage.DOKill();
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0f, 2f)
            .OnKill(() =>
            {
                DayManager.Instance.IncreaseDay();
                PlayerController.AllowMovement();
                LoadingScreen.Instance.LoadScene("Orleans");
            });
    }

    private IEnumerator TypeEndingUI(int startValue, int endValue, TextMeshProUGUI text)
    {
        _doneCoroutineCount++;

        int currentValue = startValue;

        while (currentValue != endValue)
        {
            currentValue++;
            text.text = currentValue.ToString();

            yield return new WaitForSeconds(0.02f);
        }

        _doneCoroutineCount--;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CloseDayEndingUI();
    }

    private void SkipTyping()
    {
        _currentMoneyText.text = PlayerManager.Instance().Money.ToString();
        _lastRentText.text = Mathf.Clamp(DayManager.Instance.MonthlyRent - PlayerManager.Instance().Money, 0,
            Int32.MaxValue).ToString();
    }
}
