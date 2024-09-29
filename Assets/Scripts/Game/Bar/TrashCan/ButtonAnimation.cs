using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOScale(new Vector3(0.95f, 0.95f, 1f), 0.06f))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.05f));
    }
}
