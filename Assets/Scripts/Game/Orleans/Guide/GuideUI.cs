using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideUI : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _guideIconSprites;

    [SerializeField]
    private string[] _guideStrings;

    [SerializeField]
    private Image _guideIcon;

    [SerializeField]
    private TextMeshProUGUI _guideText;

    [SerializeField]
    private CanvasGroup _canvasGroup;
    
    public enum EGuideType
    {
        GOBAR,
        RENT,
        CHANNEL,
        LOCK
    }

    public void ShowGuideUI(EGuideType guideType)
    {
        _guideIcon.sprite = _guideIconSprites[(int)guideType];
        _guideIcon.SetNativeSize();
        _guideText.text = _guideStrings[(int)guideType];

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_canvasGroup.DOFade(1f, 0.8f))
            .AppendInterval(3f)
            .OnKill(() =>
            {
                HideGuideUI();
            });
    }
    
    private void HideGuideUI()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, 0.8f);
    }
}
