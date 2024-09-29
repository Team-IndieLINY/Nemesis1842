using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class BarGameManager : MonoBehaviour
{
    [SerializeField]
    private BarGuestDB _barGuestDB;
    
    [SerializeField]
    private Guest _guest;
    
    [SerializeField]
    private RectTransform _guideLineRectTransform;
    
    [SerializeField]
    private RectTransform _guideLineEnterPointRectTransform;

    [Header("타임라인")]
    [SerializeField]
    private TimelineAsset _appearGuestTimeline;

    [SerializeField]
    private TimelineAsset _startScanTimeline;

    [SerializeField]
    private TimelineAsset _enterCocktailMakingScreenTimeline;

    [SerializeField]
    private TimelineAsset _exitCocktailMakingScreenTimeline;

    [SerializeField]
    private TimelineAsset _showDrunkGaugeTimeline;

    [SerializeField]
    private TimelineAsset _hideDrunkGaugeTimeline;
    
    [Header("Manager")]
    [SerializeField]
    private BarDialogueManager _barDialogueManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;
    
    private List<BarGuestEntity> _guestDatas;
    private List<CharacterData> _characterDatas;
    private List<StepEntity> _stepDatas;

    private PlayableDirector _playableDirector;

    private bool _isScanningDone;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _characterDatas = Resources.LoadAll<CharacterData>("GameData/CharacterData").ToList();
    }

    private void Start()
    {
        LoadGuestData();
        StartCoroutine(RoutineBar());
    }

    public void OnClickCocktailMakingButton()
    {
        _isScanningDone = true;
    }
    
    private void LoadGuestData()
    {
        _guestDatas = _barGuestDB.Guests
            .Where(x=> x.day == DayManager.Instance.Day)
            .OrderBy(x => x.order)
            .ToList();
    }

    private IEnumerator RoutineBar()
    {
        foreach (var guestData in _guestDatas)
        {
            _stepDatas = _barGuestDB.Steps
                .Where(x => x.guest_code == guestData.guest_code)
                .ToList();

            AppearGuest(guestData);
            
            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _appearGuestTimeline
                && _playableDirector.state == PlayState.Paused);

            ShowScripts(0, guestData, _guest.DrunkType);

            yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);
            
            for (int i = 1; i <= _stepDatas.Count; i++)
            {
                _guest.StepData = _stepDatas[i - 1];
                
                StartScan();

                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _startScanTimeline
                    && _playableDirector.state == PlayState.Paused);

                yield return new WaitUntil(() =>
                    _isScanningDone == true);

                EnterCocktailMakingScreen();
                
                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _enterCocktailMakingScreenTimeline
                    && _playableDirector.state == PlayState.Paused);
                
                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _exitCocktailMakingScreenTimeline
                    && _playableDirector.state == PlayState.Paused);

                ShowDrunkGauge();
                
                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _showDrunkGaugeTimeline
                    && _playableDirector.state == PlayState.Paused);

                float drunkAmount = CalculateDrunkAmount(guestData, i);
                _guest.UpdateDrunkGauge((int)drunkAmount);

                yield return new WaitForSeconds(1f);

                HideDrunkGauge();
                
                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _hideDrunkGaugeTimeline
                    && _playableDirector.state == PlayState.Paused);
                
                ShowScripts(i, guestData, _guest.DrunkType);
                
                yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);

                ResetStep();
            }
        }
    }
    
    private void AppearGuest(BarGuestEntity guestData)
    {
        List<CharacterData> characterData = _characterDatas
            .Where(x => x.CharacterCode == guestData.character_code)
            .ToList();
        
        _guest.SetGuest(characterData[0]);
        
        _guest.UpdateDrunkGauge(guestData.start_drunk_amount);
        
        _playableDirector.playableAsset = _appearGuestTimeline;
        _playableDirector.Play();
    }
    
    private void ShowScripts(int stepOrder, BarGuestEntity guestData, Guest.EDrunkType drunkType)
    {
        List<BarDialogueEntity> barDialogueEntity
            = _barGuestDB.Scripts
                .Where(x => x.guest_code == guestData.guest_code && x.step_order == stepOrder &&
                            x.drunk_type == (int)drunkType)
                .ToList();
        
        _barDialogueManager.StartDialogue(barDialogueEntity);
    }

    private void StartScan()
    {
        _playableDirector.playableAsset = _startScanTimeline;
        _playableDirector.Play();
    }

    private void EnterCocktailMakingScreen()
    {
        _playableDirector.playableAsset = _enterCocktailMakingScreenTimeline;
        _playableDirector.Play();

        _guideLineRectTransform.DOAnchorPos(_guideLineEnterPointRectTransform.anchoredPosition, 0.5f);
    }

    public void ExitCocktailMakingScreen()
    {
        _playableDirector.playableAsset = _exitCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    private void ShowDrunkGauge()
    {
        _playableDirector.playableAsset = _showDrunkGaugeTimeline;
        _playableDirector.Play();
    }

    private void HideDrunkGauge()
    {
        _playableDirector.playableAsset = _hideDrunkGaugeTimeline;
        _playableDirector.Play();
    }

    private float CalculateDrunkAmount(BarGuestEntity guestData, int stepOrder)
    {
        float drunkAmount = 0f;
        
        List<StepEntity> stepDatas = 
            _barGuestDB.Steps
            .Where(x => x.guest_code == guestData.guest_code && x.step_order == stepOrder)
            .ToList();

        if (_cocktailMakingManager.Cocktail.Alcohol < stepDatas[0].min_alcohol)
        {
            drunkAmount =
                Mathf.Clamp(_guest.DrunkAmount -
                            guestData.alcohol_per_drunk_amount * (stepDatas[0].min_alcohol - _cocktailMakingManager.Cocktail.Alcohol)
                    , 0, 100);
        }
        else if (_cocktailMakingManager.Cocktail.Alcohol >= stepDatas[0].min_alcohol && _cocktailMakingManager.Cocktail.Alcohol < stepDatas[0].max_alcohol)
        {
            drunkAmount = Mathf.Clamp(_guest.DrunkAmount + guestData.alcohol_per_drunk_amount * _cocktailMakingManager.Cocktail.Alcohol
                , 0, 100);
        }
        else if (_cocktailMakingManager.Cocktail.Alcohol >= stepDatas[0].max_alcohol)
        {
            drunkAmount = 100;
        }

        return drunkAmount;
    }

    private void ResetStep()
    {
        _cocktailMakingManager.ActivateDoneAndResetButton();
        _isScanningDone = false;
    }
}