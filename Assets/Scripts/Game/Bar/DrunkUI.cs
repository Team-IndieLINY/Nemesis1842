using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DrunkUI : MonoBehaviour
{
    [SerializeField]
    private Guest _guest;
    [SerializeField]
    private RectTransform _drunkGaugeArrowRectTransform;

    [SerializeField]
    private TextMeshProUGUI _drunkTypeText;
    
    private float _drunkAmountPerArrowDegree;

    [SerializeField]
    private float _drunkUIAnimationTime = 0.5f;

    private Dictionary<Guest.EDrunkType, string> _drunkTypeByTypeText
        = new()
        {
            { Guest.EDrunkType.Normal , "멀쩡함"},
            { Guest.EDrunkType.Tipsy , "조금 취함"},
            { Guest.EDrunkType.Drunk , "취함"},
            { Guest.EDrunkType.Nausea , "만취"},
            { Guest.EDrunkType.Wasted , "인사불성"}
        };
    
    private void Awake()
    {
        _drunkAmountPerArrowDegree = 180 * 0.01f;
    }

    public void UpdateDrunkUI()
    {
        _drunkTypeText.text = _drunkTypeByTypeText[_guest.DrunkType];

        Vector3 rotation = _drunkGaugeArrowRectTransform.eulerAngles;

        _drunkGaugeArrowRectTransform.DORotate(
            new Vector3(rotation.x, rotation.y, _drunkAmountPerArrowDegree * (100 - _guest.DrunkAmount) - 90)
            , _drunkUIAnimationTime);
    }
}
