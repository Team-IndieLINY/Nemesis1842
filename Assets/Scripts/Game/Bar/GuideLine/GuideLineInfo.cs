using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideLineInfo : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    private ScanData _scanData;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        
        GameObject scanCardObject = ScanCardPool.Inst.GetScanCardInPool();
        
        if (scanCardObject.TryGetComponent(out ScanCard scanCard))
        {
            scanCard.SetScanCard(_scanData);
            scanCardObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        }
        else
        {
            ScanCardPool.Inst.ReturnScanCardToPool(scanCardObject);
        }
    }
}
