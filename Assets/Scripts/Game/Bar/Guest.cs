using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetGuest(CharacterData characterData)
    {
        _spriteRenderer.sprite = characterData.CharacterSprite;
    }
}