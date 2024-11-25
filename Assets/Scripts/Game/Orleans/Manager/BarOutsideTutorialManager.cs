using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BarOutsideTutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool _useTutorial;

    [SerializeField]
    private BarOutsideTutorialPopup _barOutsideTutorialPopup;

    public void OpenTutorial()
    {
        if (_useTutorial && DayManager.Instance.TimeType == NPCData.ETimeType.Evening && DayManager.Instance.Day == 1)
        {
            PopUpUIManager.Inst.OpenUI(_barOutsideTutorialPopup);
        }
    }
}
