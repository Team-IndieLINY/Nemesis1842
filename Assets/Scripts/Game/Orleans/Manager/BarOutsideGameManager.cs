using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BarOutsideGameManager : MonoBehaviour
{
    [SerializeField]
    private BarGuestDB _barGuestDB;

    [SerializeField]
    private GameObject _npcPrefab;

    [SerializeField]
    private Transform _npcGroup;

    [SerializeField]
    private Sprite[] _skySprites;

    [SerializeField]
    private SpriteRenderer _skySpriteRenderer;

    [SerializeField]
    private GameObject[] _lightGroupGOs;

    [SerializeField]
    private Transform[] _playerSpawnPoints;

    [SerializeField]
    private PlayerController _player;
    
    private List<NPCData> _npcDatas;

    private void Awake()
    {
        foreach (var lightGroupGO in _lightGroupGOs)
        {
            lightGroupGO.SetActive(false);
        }
        _npcDatas = Resources.LoadAll<NPCData>("GameData/NPCData").ToList();
    }

    private void Start()
    {
        SetDay();
    }

    public void SetDay()
    {
        _skySpriteRenderer.sprite = _skySprites[(int)DayManager.Instance.TimeType];
        _lightGroupGOs[(int)DayManager.Instance.TimeType].SetActive(true);
        _player.transform.position = _playerSpawnPoints[(int)DayManager.Instance.TimeType].position;
        
        List<NPCData> currentNPCDatas =
            _npcDatas.Where(x => x.Day == DayManager.Instance.Day && x.TimeType == DayManager.Instance.TimeType).ToList();

        foreach (var currentNpcData in currentNPCDatas)
        {
            GameObject npcGO = Instantiate(_npcPrefab, currentNpcData.SpawnPosition, Quaternion.identity, _npcGroup);
            List<NPCScriptEntity> npcScripts =
                _barGuestDB.NPCScripts.Where(x => x.npc_code == currentNpcData.NPCCode).ToList();
            npcGO.GetComponent<NPC>().SetNPC(currentNpcData, npcScripts);
        }
    }
}
