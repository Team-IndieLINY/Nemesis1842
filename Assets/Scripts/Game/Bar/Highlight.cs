using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlight : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image _hightlightedImage;

    [SerializeField]
    private float _fadeTime = 0.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("mouse_hovered");
        _hightlightedImage.DOKill();
        _hightlightedImage.DOFade(1f, _fadeTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hightlightedImage.DOKill();
        _hightlightedImage.DOFade(0f, _fadeTime);
    }
}
