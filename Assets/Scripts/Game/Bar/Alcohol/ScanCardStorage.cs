using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class ScanCardStorage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ScanEvaluator _scanEvaluator;
    
    public bool IsHovered { get; private set; }

    public static ScanCardStorage Inst;
    
    private ScanCardSlot[] _cardSlots;
    
    private void Awake()
    {
        Inst = this;

        IsHovered = false;
        _cardSlots = new ScanCardSlot[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out ScanCardSlot scanCardSlot))
            {
                _cardSlots[i] = scanCardSlot;
            }
            else
            {
                Debug.LogError("ScanCardSlot을 가지고 있지 않은 오브젝트가 CardStorage 오브젝트 자식에 존재합니다.");
                break;
            }
        }
    }

    public void InsertScanCard(ScanCard scanCard)
    {
        for (int i = 0; i < _cardSlots.Length; i++)
        {
            //같은 타입의 카드가 없거나 슬롯이 모두 비어있다면
            if (_cardSlots[i].IsEmpty())
            {
                _cardSlots[i].AttachCard(scanCard);
                break;
            }

            if (_cardSlots[i].ScanCard.CompareScanCardType(scanCard))
            {
                //카드 변경
                _cardSlots[i].RemoveCard();
                _cardSlots[i].AttachCard(scanCard);
                break;
            }
        }
    }

    public void ResetScanCardStorage()
    {
        foreach (var cardSlot in _cardSlots)
        {
            cardSlot.RemoveCard();
        }
    }

    public void OnClickCocktailMakingButton()
    {
        _scanEvaluator.EvaluateScan(_cardSlots);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
    }
}