using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class HeartbeatScanner : MonoBehaviour
{
    [SerializeField]
    private Transform _scannerResetPoint;

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private RawImage _heartbeatRenderTextureImage;

    [SerializeField]
    private float _noiseFactor;

    [SerializeField]
    private Animator _heartbeatAnimator;

    [SerializeField]
    private string[] _heartbeatAnimationStrings;

    private BoxCollider2D _boxCollider2D;
    private float _hoveredTime = 0f;
    
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SetHeartbeatScanner()
    {
        _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", 0f);
    }

    public void ResetHeartbeatScanner()
    {
        _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", 0f);

        foreach (var heartbeatAnimationString in _heartbeatAnimationStrings)
        {
            if (heartbeatAnimationString == "")
            {
                continue;
            }
            
            _heartbeatAnimator.SetBool(heartbeatAnimationString, false);
        }
    }

    private void OnMouseDown()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = -1f;

        _boxCollider2D.enabled = false;

        transform.position = currentMousePosition;
        
        float distanceOfScannerToHeart = Vector2.Distance(transform.position, _guest.HeartPosition);
        _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", distanceOfScannerToHeart * _noiseFactor);
    }
    
    private void OnMouseDrag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = -1f;

        transform.position = currentMousePosition;
        
        if (_guest.IsHovered is false)
        {
            _hoveredTime = -0.75f;
            _heartbeatAnimator.SetBool(_heartbeatAnimationStrings[(int)ScanManager.Inst.AnswerHeartbeatType], false);
            _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", 0f);
        }
        else
        {
            _hoveredTime += Time.deltaTime;
            if (_hoveredTime > 0.65f)
            {
                _hoveredTime = -1.41f;
                AudioManager.Inst.PlaySFX("pulse_1");
            }
            
            _heartbeatAnimator.SetBool(_heartbeatAnimationStrings[(int)ScanManager.Inst.AnswerHeartbeatType], true);
        
            float distanceOfScannerToHeart = Vector2.Distance(transform.position, _guest.HeartPosition);

            if (distanceOfScannerToHeart < 0.6f)
            {
                distanceOfScannerToHeart = 0f;
            }
            _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", distanceOfScannerToHeart * _noiseFactor);
        }
    }

    private void OnMouseUp()
    {
        _hoveredTime = 0;
        
        transform.position = _scannerResetPoint.position;
        
        _boxCollider2D.enabled = true;
        
        _heartbeatRenderTextureImage.material.SetFloat("_NoiseScale", 0f);
        _heartbeatAnimator.SetBool(_heartbeatAnimationStrings[(int)ScanManager.Inst.AnswerHeartbeatType], false);
    }
}
