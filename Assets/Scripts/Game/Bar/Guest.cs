using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer),typeof(PlayableDirector))]
public class Guest : MonoBehaviour
{
    public enum EDrunkType
    {
        Normal, //멀쩡한
        Tipsy, //알딸딸한
        Drunk, //취한
        Nausea, //토할 것 같은
        Wasted //꽐라
    }

    [SerializeField]
    private DrunkUI _guestScreenDrunkUI;
    
    [SerializeField]
    private DrunkUI _cocktailMakingScreenDrunkUI;

    [SerializeField]
    private ConditionScanTool _conditionScanTool;

    [SerializeField]
    private RectTransform _guideLineRectTransform;
    [SerializeField]
    private RectTransform _guideLineEnterPointRectTransform;
    
    private int _drunkAmount;
    public int DrunkAmount => _drunkAmount;
    
    public StepEntity StepData { get; set; }
    
    private SpriteRenderer _spriteRenderer;
    private PlayableDirector _playableDirector;
    

    private BarGuestEntity _guestData;

    
    public EDrunkType DrunkType { get; private set; }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void SetGuest(CharacterData characterData)
    {
        _spriteRenderer.sprite = characterData.CharacterSprite;
    }

    public void UpdateDrunkGauge(int drunkAmount)
    {
        _drunkAmount = drunkAmount;

        if (_drunkAmount >= 0 && _drunkAmount <= 19)
        {
            DrunkType = EDrunkType.Normal;
        }
        else if (_drunkAmount > 19 && _drunkAmount <= 39)
        {
            DrunkType = EDrunkType.Tipsy;
        }
        else if (_drunkAmount > 39 && drunkAmount <= 59)
        {
            DrunkType = EDrunkType.Drunk;
        }
        else if (_drunkAmount > 59 && _drunkAmount <= 79)
        {
            DrunkType = EDrunkType.Nausea;
        }
        else if (_drunkAmount > 79 && _drunkAmount <= 100)
        {
            DrunkType = EDrunkType.Wasted;
        }
        
        _guestScreenDrunkUI.UpdateDrunkUI();
        _cocktailMakingScreenDrunkUI.UpdateDrunkUI();
    }

    private void OnMouseEnter()
    {
        if (_conditionScanTool.gameObject.activeSelf)
        {
            _conditionScanTool.UpdateConditionScanUI();

            if (_guideLineEnterPointRectTransform.anchoredPosition ==
                _guideLineRectTransform.anchoredPosition)
            {
                _playableDirector.Play();
            }
        }
    }

    private void OnMouseExit()
    {
        if (_conditionScanTool.gameObject.activeSelf)
        {
            _conditionScanTool.ResetConditionScanUI();
        }
    }
}