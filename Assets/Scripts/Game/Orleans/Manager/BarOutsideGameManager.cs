using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class BarOutsideGameManager : MonoBehaviour
{
    [SerializeField]
    private BarGuestDB _barGuestDB;

    [SerializeField]
    private GameObject _npcPrefab;

    [SerializeField]
    private GameObject _cutSceneTriggerPrefab;

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

    [SerializeField]
    private CanvasGroup _dayStartPanelCanvasGroup;

    [SerializeField]
    private Image _dayStartImage;
    
    [SerializeField]
    private Sprite[] _dayStartSprites;

    [SerializeField]
    private GameObject[] _extraNPCs;

    [SerializeField]
    private GuideUI _guideUI;
    
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
        _dayStartImage.sprite = _dayStartSprites[DayManager.Instance.Day - 1];
        SetDay();
    }

    public void SetDay()
    {
        if(DayManager.Instance.TimeType == NPCData.ETimeType.Evening)
        {
            _dayStartPanelCanvasGroup.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            
            sequence.PrependInterval(1f)
                .Append(_dayStartPanelCanvasGroup.DOFade(0f, 2f))
                .OnKill(() =>
                {
                    _dayStartPanelCanvasGroup.interactable = false;

                    if (DayManager.Instance.Day == 2)
                    {
                        _guideUI.ShowGuideUI(GuideUI.EGuideType.RENT);
                    }
                });
            
            AudioManager.Inst.FadeInMusic("evening");

            foreach (var extraNPC in _extraNPCs)
            {
                extraNPC.SetActive(true);
            }
        }
        else if (DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
        {
            _dayStartPanelCanvasGroup.gameObject.SetActive(false);
            AudioManager.Inst.FadeInMusic("dawn");
            
            foreach (var extraNPC in _extraNPCs)
            {
                extraNPC.SetActive(false);
            }
            
            if (DayManager.Instance.Day == 1)
            {
                _guideUI.ShowGuideUI(GuideUI.EGuideType.CHANNEL);
            }
        }
        
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
