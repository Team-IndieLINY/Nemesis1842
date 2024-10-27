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
    private string _sceneName;

    [SerializeField]
    private NPCData.ETimeType _interactableTimeType;

    [SerializeField]
    private Image _fadeImage;
    
    public void Interact()
    {
        if (DayManager.Instance.TimeType == _interactableTimeType)
        {
            _fadeImage.DOFade(1f, 1f)
                .OnKill(() =>
                {
                    SceneManager.LoadScene(_sceneName);
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
            _tooltipGO.SetActive(true);
        }
    }

    public void HideInteractableUI()
    {
        _tooltipGO.SetActive(false);
    }
}
