using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReceiptPrinter : MonoBehaviour
{
    [SerializeField]
    private Color _receiptPrinterColor;

    [SerializeField]
    private float _turnedOnReceiptPrinterTime = 0.5f;
    
    [SerializeField]
    private float _turnedOffReceiptPrinterTime = 0.5f;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        Color tempColor = _receiptPrinterColor * 1f;
        GetComponent<Image>().material.SetColor("_EmissionColor", tempColor);
    }

    public void AnimateTurnedOnReceiptPrinter()
    {
        StartCoroutine(CoroutineAnimateTurnedOnReceiptPrinter());
    }

    private IEnumerator CoroutineAnimateTurnedOnReceiptPrinter()
    {
        float elapsedTime = 0f;

        while (elapsedTime <= _turnedOnReceiptPrinterTime)
        {
            float intensity = Mathf.Lerp(0f, 20f, elapsedTime / _turnedOnReceiptPrinterTime);
            Color tempColor = _receiptPrinterColor * intensity;
            _image.material.SetColor("_EmissionColor", tempColor);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    public void AnimateTurnedOffReceiptPrinter()
    {
        StartCoroutine(CoroutineAnimateTurnedOffReceiptPrinter());
    }
    
    private IEnumerator CoroutineAnimateTurnedOffReceiptPrinter()
    {
        float elapsedTime = 0f;

        while (elapsedTime <= _turnedOffReceiptPrinterTime)
        {
            float intensity = Mathf.Lerp(20f, 0f, elapsedTime / _turnedOffReceiptPrinterTime);
            Color tempColor = _receiptPrinterColor * intensity;
            _image.material.SetColor("_EmissionColor", tempColor);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
