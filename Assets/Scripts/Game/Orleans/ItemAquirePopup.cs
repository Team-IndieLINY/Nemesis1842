using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAquirePopup : MonoBehaviour, IPopUpable
{
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TextMeshProUGUI _itemText;

    private void Awake()
    {
        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        PlayerController.RestrictMovement();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
        PlayerController.AllowMovement();
    }
}
