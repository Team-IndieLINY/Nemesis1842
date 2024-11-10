using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpUIManager : MonoBehaviour
{
    public static PopUpUIManager Inst { get; private set; }

    [SerializeField]
    private PausePanel _pausePanel;
    
    private Stack<IPopUpable> _popUpUIStack = new();

    private void Awake()
    {
        Inst = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void OpenUI(IPopUpable popUpUI)
    {
        popUpUI.ShowUI();
        _popUpUIStack.Push(popUpUI);
    }

    public void CloseUI()
    {
        if (_popUpUIStack.Count == 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName is "Bar" or "Orleans")
            {
                OpenUI(_pausePanel);
                //OpenPauseUI
            }
            return;
        }
        
        IPopUpable iPopUpAble = _popUpUIStack.Pop();
        iPopUpAble.HideUI();
    }
}
