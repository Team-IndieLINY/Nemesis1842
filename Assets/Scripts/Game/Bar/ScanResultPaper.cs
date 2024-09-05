using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;using UnityEngine.UI;

[RequireComponent(typeof(RectTransform),typeof(Image))]
public class ScanResultPaper : MonoBehaviour, IPointerDownHandler,IPointerMoveHandler,IPointerUpHandler
{
    [SerializeField]
    private TextMeshProUGUI _scanResultText;

    private RectTransform _rectTransform;
    private Image _image;

    private bool _isClicked;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }


    private Vector2 _distanceOfMousePointerToPaper;
    
    public void SetScanResultPaper(string scanResultScript)
    {
        _scanResultText.text = scanResultScript;
        
        transform.SetAsLastSibling();
    }

    public void ResetResultPaper()
    {
        _image.raycastTarget = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
        
        _distanceOfMousePointerToPaper.x = Input.mousePosition.x - _rectTransform.anchoredPosition.x;
        _distanceOfMousePointerToPaper.y = Input.mousePosition.y - _rectTransform.anchoredPosition.y;
        transform.SetAsLastSibling();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_isClicked is false)
        {
            return;
        }

        var parent = transform.parent;

        _rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(
                Input.mousePosition.x - _distanceOfMousePointerToPaper.x,
                -(parent.GetComponent<RectTransform>().sizeDelta.x / 4),
                parent.GetComponent<RectTransform>().sizeDelta.x / 2),
            Mathf.Clamp(
                Input.mousePosition.y - _distanceOfMousePointerToPaper.y,
                -(parent.GetComponent<RectTransform>().sizeDelta.y / 2),
                parent.GetComponent<RectTransform>().sizeDelta.y / 2)
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClicked = false;
    }
}
