using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ConditionScanTool : MonoBehaviour
{
    [SerializeField]
    private Transform _arrowTransform;
    
    [SerializeField]
    private float _arrowAnimationTime = 0.5f;

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private Transform _scannerResetPoint;

    private PolygonCollider2D _polygonCollider2D;
    private float _conditionValuePerArrowDegree;
    
    public bool IsClicked { get; private set; }
    
    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _conditionValuePerArrowDegree = 120 * 0.01f;
        
        var eulerAngles = _arrowTransform.eulerAngles;
        eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, 60f);
        _arrowTransform.eulerAngles = eulerAngles;
    }

    public void UpdateConditionScanUI()
    {
        Vector3 rotation = _arrowTransform.eulerAngles;
        
        _arrowTransform.DORotate(
            new Vector3(rotation.x, rotation.y,
                _conditionValuePerArrowDegree * (100 - _guest.StepData.condition_value) - 60), _arrowAnimationTime)
            .SetEase(Ease.OutBounce);
    }

    public void ResetConditionScanUI()
    {
        Vector3 rotation = _arrowTransform.eulerAngles;
        _arrowTransform.DORotate(
            new Vector3(rotation.x, rotation.y,
                60), _arrowAnimationTime).SetEase(Ease.OutBounce);
    }

    private void OnMouseDown()
    {
        IsClicked = true;
        _polygonCollider2D.enabled = false;
        
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = -1f;

        transform.position = currentMousePosition;
    }
    
    private void OnMouseDrag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = -1f;

        transform.position = currentMousePosition;
    }

    private void OnMouseUp()
    {
        _polygonCollider2D.enabled = true;
        IsClicked = false;
        ResetConditionScanUI();
        transform.position = _scannerResetPoint.position;
    }
}