using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField]
    private ItemEquipmentSlotHandler _itemEquipmentSlotHandler;

    [SerializeField]
    private AlcoholController _alcoholController;
    
    private Image _image;
    private Item _item;

    public static bool IsClicked;

    private void Awake()
    {
        _image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.ItemData.ItemSprite;
        _image.SetNativeSize();
    }

    private void Update()
    {
        if (IsClicked == true && Input.GetMouseButtonUp(0))
        {
            IsClicked = false;
            gameObject.SetActive(false);

            if (_itemEquipmentSlotHandler.IsHovered)
            {
                _alcoholController.EquipItem(_item);
                return;
            }
        
            _item.IncreaseAmount(1);
        }

        if (IsClicked == true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }
}
