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
    
    [SerializeField]
    private ConditionScanTool _conditionScanTool;
    
    public StepEntity StepData { get; set; }

    private SpriteRenderer _spriteRenderer;
    private PlayableDirector _playableDirector;
    private List<CharacterData> _characterDatas;
    private CharacterData _currentCharacterData;

    private bool isHovered;
    public bool IsHovered => isHovered;
    
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

        _currentCharacterData = characterData[0];
        _spriteRenderer.sprite = _currentCharacterData.StringByCharacterSprite.GetDict()["Normal"];
        
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    public void SetCharacterSprite(string characterSpriteCode)
    {
        _spriteRenderer.sprite = _currentCharacterData.StringByCharacterSprite.GetDict()[characterSpriteCode];
    }
    
    private void OnMouseEnter()
    {
        if (_conditionScanTool.IsClicked)
        {
            _conditionScanTool.UpdateConditionScanUI();
        }
    }

    private void OnMouseExit()
    {
        if (_conditionScanTool.IsClicked)
        {
            _conditionScanTool.ResetConditionScanUI();
        }
    }
}