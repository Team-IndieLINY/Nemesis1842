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
    private GameObject _scanSelectorGO;

    [SerializeField]
    private CocktailRecipeUI _cocktailRecipeUI;

    [SerializeField]
    private AlcoholController _alcoholController;
    
    [SerializeField]
    private GameObject _openCocktailRecipeButtonGO;

    [SerializeField]
    private GameObject _enterAlcoholPhaseButtonGO;

    [Header("타임라인")]
    [SerializeField]
    private TimelineAsset _appearGuestTimeline;

    [SerializeField]
    private TimelineAsset _enterCocktailMakingScreenTimeline;

    [SerializeField]
    private TimelineAsset _enterAlcoholPhaseTimeline;

    [SerializeField]
    private TimelineAsset _exitCocktailMakingScreenTimeline;

    [Header("Manager")]
    [SerializeField]
    private BarDialogueManager _barDialogueManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    private List<BarGuestEntity> _guestDatas;

    private List<StepEntity> _stepDatas;

    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        LoadGuestData();
        StartCoroutine(RoutineBar());
    }

    public void OnClickCocktailMakingButton()
    {
        
    }

    private void LoadGuestData()
    {
        _guestDatas = _barGuestDB.Guests
            .Where(x => x.day == DayManager.Instance.Day)
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
            
            ShowScripts(0, guestData);
            
            yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);
            
            for (int i = 1; i <= _stepDatas.Count; i++)
            {
                _guest.StepData = _stepDatas[i - 1];
                _cocktailRecipeUI.SetCocktailSummaryText(_stepDatas[i - 1].cocktail_summary_text);
                _alcoholController.SetAnswerAlcohol(_stepDatas[i - 1].max_answer_alcohol,
                    _stepDatas[i - 1].min_answer_alcohol);

                ScanManager.Inst.AnswerConditionType = (ConditionScanData.EConditionType)_stepDatas[i - 1].condition_answer;
                ScanManager.Inst.AnswerLeavenType = (LiverScanData.ELeavenType)_stepDatas[i - 1].leaven_answer;
                ScanManager.Inst.SetLiver(_stepDatas[i - 1].square_leaven_count, _stepDatas[i - 1].circle_leaven_count,
                    _stepDatas[i - 1].star_leaven_count);

                ShowScannerSelector();

                yield return new WaitUntil(() =>
                    ScanManager.Inst.IsScanningDone);

                EnterCocktailMakingScreen();

                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _enterCocktailMakingScreenTimeline
                    && _playableDirector.state == PlayState.Paused);

                yield return new WaitUntil(() =>
                    _playableDirector.playableAsset == _exitCocktailMakingScreenTimeline
                    && _playableDirector.state == PlayState.Paused);
                

                ShowScripts(i, guestData);

                yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);

                ResetStep();
            }
        }
    }

    private void AppearGuest(BarGuestEntity guestData)
    {
        _guest.SetGuest(guestData);

        _playableDirector.playableAsset = _appearGuestTimeline;
        _playableDirector.Play();
    }

    private void ShowScripts(int stepOrder, BarGuestEntity guestData)
    {
        List<BarDialogueEntity> barDialogueEntity
            = _barGuestDB.Scripts
                .Where(x => x.guest_code == guestData.guest_code &&
                            x.step_order == stepOrder)
                .ToList();

        _barDialogueManager.StartDialogue(barDialogueEntity);
    }
    
    private void ShowScannerSelector()
    {
        _scanSelectorGO.SetActive(true);
    }

    public void EnterScanPhase()
    {
        _scanSelectorGO.SetActive(false);
        
        ScanManager.Inst.EnterScanPhase();
    }

    private void EnterCocktailMakingScreen()
    {
        _playableDirector.playableAsset = _enterCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    public void OnClickEnterAlcoholPhaseButton()
    {
        _playableDirector.playableAsset = _enterAlcoholPhaseTimeline;
        _playableDirector.Play();
    }

    public void ExitCocktailMakingScreen()
    {
        _playableDirector.playableAsset = _exitCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    private void ResetStep()
    {
        _openCocktailRecipeButtonGO.SetActive(true);
        _enterAlcoholPhaseButtonGO.SetActive(true);
        _cocktailMakingManager.ResetCocktail();
        ScanManager.Inst.ResetScanManager();
        _alcoholController.ResetAlcoholController();
    }
}