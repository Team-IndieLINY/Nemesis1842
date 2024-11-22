using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenWarningPopupPanel : MonoBehaviour, IPopUpable
{
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void OnClickGoMainScreenButton()
    {
        DayManager.Instance.ResetDayManager();
        InventoryManager.Instance().ResetInventoryData();
        InventoryManager.Instance().ResetItems();
        PlayerManager.Instance().ResetPlayerData();
        PlayerManager.Instance().IsNewItemDotActivated = false;
        
        LoadingScreen.Instance.LoadScene("MainScreen");
        
        PopUpUIManager.Inst.CloseUI();
        PopUpUIManager.Inst.CloseUI();
    }

    public void OnClickBackButton()
    {
        PopUpUIManager.Inst.CloseUI();
    }
}
