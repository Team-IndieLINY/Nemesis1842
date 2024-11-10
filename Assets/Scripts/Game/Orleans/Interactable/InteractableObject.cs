using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private IInteractable _interactableImplementation;
    
    [SerializeField]
    protected GameObject _tooltipGO;
    
    [SerializeField]
    protected Image _objectIconImage;
    
    [SerializeField]
    protected Image _objectNameTextImage;

    public abstract void Interact();

    public void ShowInteractableUI()
    {
        _objectIconImage.DOKill();
        _objectNameTextImage.DOKill();

        _tooltipGO.SetActive(true);

        _objectIconImage.DOFillAmount(1f, 0.3f);
        _objectNameTextImage.DOFade(1f, 0.3f);
    }

    public void HideInteractableUI()
    {
        _objectIconImage.DOKill();
        _objectNameTextImage.DOKill();

        _objectNameTextImage.DOFade(0f, 0.3f);
        _objectIconImage.DOFillAmount(0f, 0.3f)
            .OnKill(() =>
            {
                _tooltipGO.SetActive(false);
            });
    }
}
