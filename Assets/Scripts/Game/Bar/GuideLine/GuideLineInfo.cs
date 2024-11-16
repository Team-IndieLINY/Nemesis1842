using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GuideLineInfo : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    protected ScanData _scanData;

    protected Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.raycastTarget = true;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void ResetGuideLineInfo();

    protected IEnumerator WaitEvaluationScanCoroutine()
    {
        bool isSuccess = ScanManager.Inst.EvaluateScan(_scanData);

        if (TutorialManager.Inst.UseTutorial)
        {
            // if (isSuccess is true)
            // {
            //     TutorialManager.Inst.ShowTutorialByIndex(4);
            // }
            // else
            // {
            //     TutorialManager.Inst.ShowTutorial();
            // }
            //
            // yield return new WaitUntil(() => TutorialManager.Inst.IsShowing == false);
        }

        yield return new WaitForSeconds(1f);
        
        ScanManager.Inst.ExitScanPhase();
    }
}