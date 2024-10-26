using System;
using System.Collections;
using System.Collections.Generic;
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
    private Sprite _npcSprite;

    public Sprite NPCSprite => _npcSprite;

    [SerializeField]
    private ETimeType _timeType;

    public ETimeType TimeType => _timeType;

    [SerializeField]
    private Vector2 _spawnPosition;

    public Vector2 SpawnPosition => _spawnPosition;
}