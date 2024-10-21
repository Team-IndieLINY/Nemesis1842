using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemNameText;

    [SerializeField]
    private TextMeshProUGUI _itemDescriptionText;

    public void UpdateTooltipUI(ItemData itemData)
    {
        _itemNameText.text = itemData.ItemName;
        _itemDescriptionText.text = itemData.ItemDescription;
    }
}
