using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarOutsideGameManager : MonoBehaviour
{
    [SerializeField]
    private BarGuestDB _barGuestDB;

    [SerializeField]
    private GameObject _npcPrefab;

    [SerializeField]
    private Transform _npcGroup;

    private int day = 1;
    private NPCData.ETimeType _timeType = NPCData.ETimeType.Evening;

    private List<NPCData> _npcDatas;

    private void Awake()
    {
        _npcDatas = Resources.LoadAll<NPCData>("GameData/NPCData").ToList();
        SetDay();
    }

    public void SetDay()
    {
        List<NPCData> currentNPCDatas = _npcDatas.Where(x => x.Day == day && x.TimeType == _timeType).ToList();

        foreach (var currentNpcData in currentNPCDatas)
        {
            GameObject npcGO = Instantiate(_npcPrefab, currentNpcData.SpawnPosition, Quaternion.identity, _npcGroup);
            List<NPCScriptEntity> npcScripts =
                _barGuestDB.NPCScripts.Where(x => x.npc_code == currentNpcData.NPCCode).ToList();
            npcGO.GetComponent<NPC>().SetNPC(currentNpcData, npcScripts);
        }
    }
}
