using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BarOutsideTutorialPopup : MonoBehaviour, IPopUpable
{
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    public void ShowUI()
    {
        PlayerController.RestrictMovement();
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, 0.7f);
    }

    public void HideUI()
    {
        PlayerController.AllowMovement();
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, 0.7f);
    }
}
