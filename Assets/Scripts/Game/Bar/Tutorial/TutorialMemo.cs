using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialMemo : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Image _highlightImage;

    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        if (TutorialManager.Inst.UseTutorial)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _highlightImage.gameObject.SetActive(false);
        _rectTransform.DOAnchorPos(new Vector2(-1331, 219), 0.5f);
        _rectTransform.DOScale(new Vector3(1.42744f, 1.42744f, 1.42744f), 0.5f);
    }

    public void HideMemo()
    {
        _image.DOFade(0f, 0.3f);
    }
}
