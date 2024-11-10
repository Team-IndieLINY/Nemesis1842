using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Portal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;
    
    [SerializeField]
    private Image _portalIconImage;
    
    [SerializeField]
    private Image _portalNameTextImage;

    private void Awake()
    {
        _portalIconImage.fillAmount = 0;
        _portalNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public abstract void Interact();

    public void ShowInteractableUI()
    {
        _portalIconImage.DOKill();
        _portalNameTextImage.DOKill();

        _tooltipGO.SetActive(true);

        _portalIconImage.DOFillAmount(1f, 0.3f);
        _portalNameTextImage.DOFade(1f, 0.3f);
    }

    public void HideInteractableUI()
    {
        _portalIconImage.DOKill();
        _portalNameTextImage.DOKill();

        _portalNameTextImage.DOFade(0f, 0.3f);
        _portalIconImage.DOFillAmount(0f, 0.3f)
            .OnKill(() =>
            {
                _tooltipGO.SetActive(false);
            });
    }
}
