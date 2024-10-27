using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;

    [SerializeField]
    private string _sceneName;
    
    public void Interact()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void ShowInteractableUI()
    {
        _tooltipGO.SetActive(true);
    }

    public void HideInteractableUI()
    {
        _tooltipGO.SetActive(false);
    }
}
