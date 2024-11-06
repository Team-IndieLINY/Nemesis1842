using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeartbeatGuideLineInfo : GuideLineInfo,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image _highlightImage;
    
    [SerializeField]
    private Image _selectFrameImage;
    public override void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("check");
        StartCoroutine(WaitEvaluationScanCoroutine());
    }

    public override void ResetGuideLineInfo()
    {
        _highlightImage.color = new Color32(255, 255, 255, 255);
        _selectFrameImage.color = new Color32(255, 255, 255, 255);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlightImage.DOKill();
        _selectFrameImage.DOKill();
        
        _highlightImage.DOFade(1f, 0.2f);
        _selectFrameImage.DOFade(1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _highlightImage.DOKill();
        _selectFrameImage.DOKill();
        
        _highlightImage.DOFade(0f, 0.2f);
        _selectFrameImage.DOFade(0f, 0.2f);
    }
}