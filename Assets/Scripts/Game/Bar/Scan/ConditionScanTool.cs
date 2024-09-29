using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ConditionScanTool : MonoBehaviour
{
    [SerializeField]
    private RectTransform _arrowRectTransform;
    
    [SerializeField]
    private float _arrowAnimationTime = 0.5f;

    [SerializeField]
    private Guest _guest;

    private RectTransform _rectTransform;

    private float _conditionValuePerArrowDegree;
    
    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _conditionValuePerArrowDegree = 120 * 0.01f;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
                Cursor.visible = true;
            }
            _rectTransform.anchoredPosition = Input.mousePosition;
        }

    }

    public void ShowTool()
    {
        Cursor.visible = false;
        gameObject.SetActive(true);
    }

    public void UpdateConditionScanUI()
    {
        Vector3 rotation = _arrowRectTransform.eulerAngles;
        _arrowRectTransform.DORotate(
            new Vector3(rotation.x, rotation.y,
                _conditionValuePerArrowDegree * (100 - _guest.StepData.condition_value) - 60), _arrowAnimationTime)
            .SetEase(Ease.OutBounce);
    }

    public void ResetConditionScanUI()
    {
        Vector3 rotation = _arrowRectTransform.eulerAngles;
        _arrowRectTransform.DORotate(
            new Vector3(rotation.x, rotation.y,
                60), _arrowAnimationTime).SetEase(Ease.OutBounce);
    }
}