using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiverScanTool : MonoBehaviour
{

    [SerializeField]
    private Transform _scannerResetPoint;
    
    private void OnMouseDown()
    {
        
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0f;

        transform.position = currentMousePosition;
    }
    
    private void OnMouseDrag()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0f;

        transform.position = currentMousePosition;
    }

    private void OnMouseUp()
    {
        transform.position = _scannerResetPoint.position;
    }
}
