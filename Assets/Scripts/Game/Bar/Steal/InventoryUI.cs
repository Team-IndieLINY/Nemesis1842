using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Inventory))]
public class InventoryUI : MonoBehaviour,IPopUpable
{
    [SerializeField]
    private TextMeshProUGUI _itemNameText;

    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TextMeshProUGUI _itemDescriptionText;
    
    [SerializeField]
    private GameObject _closeButtonGO;


    [SerializeField]
    private GameObject _itemDescriptionPanelGO;
    [SerializeField]
    private GameObject _itemDescriptionGroupGO;

    [SerializeField]
    private GameObject _smallBackgroundGO;

    [SerializeField]
    private GameObject _bigBackgroundGO;

    [SerializeField]
    private Button _closeInventoryButton;
    
    private CanvasGroup _canvasGroup;
    private Inventory _inventory;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _inventory = GetComponent<Inventory>();

        _itemDescriptionGroupGO.SetActive(false);
        _canvasGroup.alpha = 0f;
    }

    public void UpdateItemDescriptionPanel()
    {
        if (_inventory.CurrentSelectedSlot == null)
        {
            _itemDescriptionGroupGO.SetActive(false);
        }
        
        _itemNameText.text = _inventory.CurrentSelectedSlot.InformationItemData.ItemName;
        _itemDescriptionText.text = _inventory.CurrentSelectedSlot.InformationItemData.ItemDescription;
        
        _itemImage.sprite = _inventory.CurrentSelectedSlot.InformationItemData.ItemSprite;
        _itemImage.SetNativeSize();
        
        var sizeDelta = _itemImage.rectTransform.sizeDelta;
        sizeDelta = new Vector2(sizeDelta.x * 2,
            sizeDelta.y * 2);
        _itemImage.rectTransform.sizeDelta = sizeDelta;

        _itemDescriptionGroupGO.SetActive(true);
    }

    public void OpenInventoryUI()
    {
        _closeInventoryButton.interactable = true;
        AudioManager.Inst.PlaySFX("mouse_click");
        _closeButtonGO.SetActive(true);
        gameObject.SetActive(true);
        _itemDescriptionPanelGO.SetActive(true);
        
        _smallBackgroundGO.SetActive(false);
        _bigBackgroundGO.SetActive(true);
        
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, 0.3f);
    }
    
    public void CloseInventoryUI()
    {
        _closeInventoryButton.interactable = false;
        AudioManager.Inst.PlaySFX("mouse_click");
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, 0.3f)
            .OnKill(() =>
            {
                gameObject.SetActive(false);
            });
    }
    
    public void OpenStealInventoryUI()
    {
        _closeButtonGO.SetActive(false);
        gameObject.SetActive(true);
        _itemDescriptionPanelGO.SetActive(false);
        
        _smallBackgroundGO.SetActive(true);
        _bigBackgroundGO.SetActive(false);
        
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, 0.3f);
    }
    
    public void CloseStealInventoryUI()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, 0.3f)
            .OnKill(() =>
            {
                gameObject.SetActive(false);
            });
    }

    public void ShowUI()
    {
        PlayerController.RestrictMovement();
        OpenInventoryUI();
    }

    public void HideUI()
    {
        PlayerController.AllowMovement();
        CloseInventoryUI();
    }
}