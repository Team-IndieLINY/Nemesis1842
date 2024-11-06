using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterData",order = Int32.MaxValue)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private RuntimeAnimatorController _characterMouseAnimatorController;

    public RuntimeAnimatorController CharacterMouseAnimatorController => _characterMouseAnimatorController;

    [SerializeField]
    private RuntimeAnimatorController _characterEyeAnimatorController;
    public RuntimeAnimatorController CharacterEyeAnimatorController => _characterEyeAnimatorController;
    
    [SerializeField]
    private string _characterCode;

    public string CharacterCode => _characterCode;
    
    [SerializeField]
    private string _characterName;

    public string CharacterName => _characterName;
    
    [SerializeField]
    private Vector2 _heartPoint;

    public Vector2 HeartPoint => _heartPoint;

    [SerializeField]
    private SerializableDict<Sprite> _stringByCharacterSprite;

    public SerializableDict<Sprite> StringByCharacterSprite => _stringByCharacterSprite;
}