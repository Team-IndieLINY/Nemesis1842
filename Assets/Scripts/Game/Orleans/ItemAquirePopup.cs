using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAquirePopup : MonoBehaviour, IPopUpable
{
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TextMeshProUGUI _itemText;

    [SerializeField]
    private Image _anyKeyImage;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown && gameObject.activeSelf == true)
        {
            HideUI();
        }
    }

    public void SetAquireItem(ItemData itemData)
    {
        _itemImage.sprite = itemData.ItemSprite;
        _itemImage.SetNativeSize();

        _itemText.text = "<color=#c89e38>\"" + itemData.ItemName + "\"</color>을(를) 획득하였습니다.";
    }
    
    public void SetEarnMoney(MoneyItemData moneyItemData)
    {
        _itemImage.sprite = moneyItemData.ItemSprite;
        _itemImage.SetNativeSize();

        _itemText.text = "\"" + moneyItemData.Money + "\" $ 을(를) 획득하였습니다.";
    }
    
    public void ShowUI()
    {
        AudioManager.Inst.PlaySFX("item_aquire");
        _anyKeyImage.DOKill();
        _anyKeyImage.DOFade(0f, 0.8f).SetLoops(-1, LoopType.Yoyo);
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
