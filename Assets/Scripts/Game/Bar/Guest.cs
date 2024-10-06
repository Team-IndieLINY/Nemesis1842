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
    public enum EAlcoholReactionType
    {
        LOW = 0,
        FIT,
        HIGH
    }
    
    [SerializeField]
    private ConditionScanTool _conditionScanTool;

    [SerializeField]
    private RectTransform _guideLineRectTransform;
    [SerializeField]
    private RectTransform _guideLineEnterPointRectTransform;

    [SerializeField]
    private GameObject[] _hpGOs;
    
    public StepEntity StepData { get; set; }

    public EAlcoholReactionType AlcoholReactionType { get; set; }

    private SpriteRenderer _spriteRenderer;
    private PlayableDirector _playableDirector;
    private List<CharacterData> _characterDatas;
    
    private int _hpCount;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playableDirector = GetComponent<PlayableDirector>();
        _characterDatas = Resources.LoadAll<CharacterData>("GameData/CharacterData").ToList();
    }

    public void SetGuest(BarGuestEntity guestData)
    {
        List<CharacterData> characterData = _characterDatas
            .Where(x => x.CharacterCode == guestData.character_code)
            .ToList();

        _hpCount = guestData.hp_count;

        for (int i = 0; i < _hpGOs.Length; i++)
        {
            if (i < _hpCount)
            {
                _hpGOs[i].SetActive(true);
            }
            else
            {
                _hpGOs[i].SetActive(false);
            }
        }
        
        _spriteRenderer.sprite = characterData[0].CharacterSprite;
    }

    public void DecreaseHPCount()
    {
        _hpCount--;

        for (int i = 0; i < _hpGOs.Length; i++)
        {
            if (i < _hpCount)
            {
                _hpGOs[i].SetActive(true);
            }
            else
            {
                _hpGOs[i].SetActive(false);
            }
        }
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