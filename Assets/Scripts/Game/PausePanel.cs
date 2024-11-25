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
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void OnClickResumeButton()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        PopUpUIManager.Inst.CloseUI();
    }
    
    public void OnClickSettingButton()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        PopUpUIManager.Inst.OpenUI(_settingPanel);
    }
    
    public void OnClickMainScreenButton()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
        PopUpUIManager.Inst.OpenUI(_mainScreenWarningPopupPanel);
    }
}
