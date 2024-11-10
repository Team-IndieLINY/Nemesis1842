using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour,IPopUpable
{
    [SerializeField]
    private SettingPanel _settingPanel;

    [SerializeField]
    private MainScreenWarningPopupPanel _mainScreenWarningPopupPanel;
    
    public void ShowUI()
    {
        PlayerController.RestrictMovement();
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        PlayerController.AllowMovement();
        gameObject.SetActive(false);
    }

    public void OnClickResumeButton()
    {
        PopUpUIManager.Inst.CloseUI();
    }
    
    public void OnClickSettingButton()
    {
        PopUpUIManager.Inst.OpenUI(_settingPanel);
    }
    
    public void OnClickMainScreenButton()
    {
        PopUpUIManager.Inst.OpenUI(_mainScreenWarningPopupPanel);
    }
}
