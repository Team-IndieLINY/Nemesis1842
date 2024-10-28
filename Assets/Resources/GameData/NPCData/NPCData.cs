using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/NPCData", fileName = "NPCData", order = Int32.MaxValue)]
public class NPCData : ScriptableObject
{
    public enum ETimeType
    {
        Evening,
        Dawn
    }
    
    [SerializeField]
    private string npcCode;

    public string NPCCode => npcCode;

    [SerializeField]
    private int _day;

    public int Day => _day;

    [SerializeField]
    private AnimatorController _animatorController;

    public AnimatorController AnimatorController => _animatorController;

    [SerializeField]
    private ETimeType _timeType;

    public ETimeType TimeType => _timeType;

    [SerializeField]
    private Vector2 _spawnPosition;

    public Vector2 SpawnPosition => _spawnPosition;
}