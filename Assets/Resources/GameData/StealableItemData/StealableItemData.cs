using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StealableItemData : ScriptableObject
{
    [SerializeField]
    protected Sprite _itemSprite;

    public Sprite ItemSprite => _itemSprite;
}