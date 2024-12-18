using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/NPCData", fileName = "NPCData", order = Int32.MaxValue)]
public class NPCData : ScriptableObject
{
    public enum ENPCType
    {
        Human,
        Object
    }
    public enum ETimeType
    {
        Evening,
        Dawn
    }
    
    [SerializeField]
    private string npcCode;

    public string NPCCode => npcCode;
    
    [SerializeField]
    private ENPCType _npcType;

    public ENPCType NPCType => _npcType;

    [SerializeField]
    private int _day;

    public int Day => _day;

    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    public RuntimeAnimatorController AnimatorController => _animatorController;

    [SerializeField]
    private ETimeType _timeType;

    public ETimeType TimeType => _timeType;
    
    [SerializeField]
    private Sprite _npcNameSprite;

    public Sprite NPCNameSprite => _npcNameSprite;

    [SerializeField]
    private Vector2 _spawnPosition;

    public Vector2 SpawnPosition => _spawnPosition;

    [SerializeField]
    private ItemData _rewardItemData;

    public ItemData RewardItemData => _rewardItemData;

    [SerializeField]
    private int _rewardItemAmount;
    public int RewardItemAmount => _rewardItemAmount;
    
    [SerializeField]
    private StealableItemData _rewardStealableItemData;

    public StealableItemData RewardStealableItemData => _rewardStealableItemData;
}