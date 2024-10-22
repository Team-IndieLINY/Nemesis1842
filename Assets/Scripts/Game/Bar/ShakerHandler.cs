using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShakerHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public static bool IsHovered { get; set; }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
    }
}
