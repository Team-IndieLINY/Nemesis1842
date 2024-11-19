using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer),typeof(PlayableDirector),typeof(Animator))]
public class Guest : MonoBehaviour
{
    
    [SerializeField]
    private ConditionScanTool _conditionScanTool;
    
    private Animator _eyeAnimator;

    [SerializeField]
    private Animator _mouthAnimator;
    
    public StepEntity StepData { get; set; }

    private SpriteRenderer _spriteRenderer;
    private PlayableDirector _playableDirector;
    private List<CharacterData> _characterDatas;
    private CharacterData _currentCharacterData;
    public CharacterData CurrentCharacterData => _currentCharacterData;

    private Vector2 _heartPosition = new();
    public Vector2 HeartPosition => _heartPosition;
    
    private bool _isHovered;
    public bool IsHovered => _isHovered;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playableDirector = GetComponent<PlayableDirector>();
        _characterDatas = Resources.LoadAll<CharacterData>("GameData/CharacterData").ToList();
        _eyeAnimator = GetComponent<Animator>();
    }

    public void SetGuest(BarGuestEntity guestData)
    {
        List<CharacterData> characterData = _characterDatas
            .Where(x => x.CharacterCode == guestData.character_code)
            .ToList();

        _currentCharacterData = characterData[0];
        _spriteRenderer.sprite = _currentCharacterData.StringByCharacterSprite.GetDict()["Normal"];
        _eyeAnimator.runtimeAnimatorController = _currentCharacterData.CharacterEyeAnimatorController;
        _mouthAnimator.runtimeAnimatorController = _currentCharacterData.CharacterMouseAnimatorController;
        _heartPosition = _currentCharacterData.HeartPoint;
        
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }

    public void SetCharacterSprite(string characterSpriteCode)
    {
        if (characterSpriteCode == "Wasted")
        {
            AudioManager.Inst.PlaySFX("drunken");
        }
        _spriteRenderer.sprite = _currentCharacterData.StringByCharacterSprite.GetDict()[characterSpriteCode];

        foreach (var stringBySprite in _currentCharacterData.StringByCharacterSprite.GetDict())
        {
            _eyeAnimator.SetBool(stringBySprite.Key, false); 
        }
        _eyeAnimator.SetBool(characterSpriteCode, true);
    }
    
    private void OnMouseEnter()
    {
        _isHovered = true;
        if (_conditionScanTool.IsClicked)
        {
            _conditionScanTool.UpdateConditionScanUI();
        }
    }

    private void OnMouseExit()
    {
        _isHovered = false;
        if (_conditionScanTool.IsClicked)
        {
            _conditionScanTool.ResetConditionScanUI();
        }
    }
}