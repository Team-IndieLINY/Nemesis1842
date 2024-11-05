using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;
    
    [SerializeField]
    private Image _portalIconImage;

    [SerializeField]
    private Image _portalNameTextImage;

    [SerializeField]
    private string _sceneName;

    [SerializeField]
    private NPCData.ETimeType _interactableTimeType;

    [SerializeField]
    private Image _fadeImage;

    private void Awake()
    {
        _portalIconImage.fillAmount = 0;
        _portalNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public void Interact()
    {
        if (DayManager.Instance.TimeType == _interactableTimeType)
        {
            if (DayManager.Instance.TimeType == NPCData.ETimeType.Evening)
            {
                AudioManager.Inst.FadeOutMusic("evening");
            }
            else if (DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
            {
                AudioManager.Inst.FadeOutMusic("dawn");
            }
            _fadeImage.DOFade(1f, 1f)
                .OnKill(() =>
                {
                    LoadingScreen.Instance.LoadScene(_sceneName);
                });
        }
        else
        {
            
        }
    }

    public void ShowInteractableUI()
    {
        if (DayManager.Instance.TimeType == _interactableTimeType)
        {
            _portalIconImage.DOKill();
            _portalNameTextImage.DOKill();
        
            _tooltipGO.SetActive(true);

            _portalIconImage.DOFillAmount(1f, 0.3f);
            _portalNameTextImage.DOFade(1f, 0.3f);
        }
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
