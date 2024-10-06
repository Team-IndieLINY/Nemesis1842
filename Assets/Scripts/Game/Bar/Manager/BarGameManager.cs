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
    private ScanEvaluator _scanEvaluator;

    [SerializeField]
    private ScanCardStorage _scanCardStorage;

    [SerializeField]
    private AlcoholCalculator _alcoholCalculator;

    [SerializeField]
    private CocktailRecipeUI _cocktailRecipeUI;
    
    [SerializeField]
    private RectTransform _guideLineRectTransform;
    
    [SerializeField]
    private RectTransform _guideLineEnterPointRectTransform;

    [SerializeField]
    private GameObject _openCocktailRecipeButtonGO;

    [SerializeField]
    private GameObject _enterAlcoholPhaseButtonGO;

    [SerializeField]
    private GameObject _exitCocktailMakingScreenButtonGO;

    [Header("타임라인")]
    [SerializeField]
    private TimelineAsset _appearGuestTimeline;

    [SerializeField]
    private TimelineAsset _startScanTimeline;

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

    private bool _isScanningDone;

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
            AppearGuest(guestData);
            
            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _appearGuestTimeline
                && _playableDirector.state == PlayState.Paused);

            ShowScripts(1, 0, guestData, Guest.EAlcoholReactionType.LOW);

            yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);


            for (int i = 1; i <= guestData.hp_count; i++)
            {
                _alcoholCalculator.ResetAlcoholCalculator();
                
                _stepDatas = _barGuestDB.Steps
                    .Where(x => x.guest_code == guestData.guest_code && i == x.hp_order)
                    .ToList();

                CocktailAlcoholAnswerEntity cocktailAlcoholAnswerData
                    = _barGuestDB.AlcoholAnswers
                        .Where(x => x.guest_code == guestData.guest_code && i == x.hp_order)
                        .ToList().First();

                _alcoholCalculator.SetAnswerAlcohol(cocktailAlcoholAnswerData.max_answer_alcohol,
                    cocktailAlcoholAnswerData.min_answer_alcohol);
                
                for (int j = 1; j <= _stepDatas.Count; j++)
                {
                    _guest.StepData = _stepDatas[j - 1];
                    _scanEvaluator.SetScanAnswer(_stepDatas[j - 1].condition_value);
                    _cocktailRecipeUI.SetCocktailSummaryText(_stepDatas[j - 1].cocktail_summary_text);
                
                    StartScan();

                    yield return new WaitUntil(() =>
                        _playableDirector.playableAsset == _startScanTimeline
                        && _playableDirector.state == PlayState.Paused);

                    yield return new WaitUntil(() =>
                        _isScanningDone == true);

                    yield return new WaitForSeconds(1f);

                    EnterCocktailMakingScreen();
                
                    yield return new WaitUntil(() =>
                        _playableDirector.playableAsset == _enterCocktailMakingScreenTimeline
                        && _playableDirector.state == PlayState.Paused);
                
                    yield return new WaitUntil(() =>
                        _playableDirector.playableAsset == _exitCocktailMakingScreenTimeline
                        && _playableDirector.state == PlayState.Paused);
                    
                    if (_guest.AlcoholReactionType == Guest.EAlcoholReactionType.FIT)
                    {
                        _guest.DecreaseHPCount();
                    }
                
                    ShowScripts(i, j, guestData, _guest.AlcoholReactionType);
                
                    yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);

                    ResetStep();
                    
                    if (_guest.AlcoholReactionType == Guest.EAlcoholReactionType.FIT)
                    {
                        break;
                    }
                }
            }

        }
    }
    
    private void AppearGuest(BarGuestEntity guestData)
    {
        _guest.SetGuest(guestData);
        
        _playableDirector.playableAsset = _appearGuestTimeline;
        _playableDirector.Play();
    }
    
    private void ShowScripts(int hpOrder, int stepOrder, BarGuestEntity guestData, Guest.EAlcoholReactionType alcoholReactionType)
    {
        List<BarDialogueEntity> barDialogueEntity
            = _barGuestDB.Scripts
                .Where(x => x.guest_code == guestData.guest_code && x.hp_order == hpOrder && x.step_order == stepOrder &&
                            x.reaction_type == (int)alcoholReactionType)
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

    public void OnClickEnterAlcoholPhaseButton()
    {
        _playableDirector.playableAsset = _enterAlcoholPhaseTimeline;
        _playableDirector.Play();
    }

    public void OnClickExitCocktailMakingScreenButton()
    {
        _playableDirector.playableAsset = _exitCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    private void ResetStep()
    {
        _openCocktailRecipeButtonGO.SetActive(true);
        _enterAlcoholPhaseButtonGO.SetActive(true);
        _exitCocktailMakingScreenButtonGO.SetActive(true);
        _scanEvaluator.ResetScanEvaluator();
        _scanCardStorage.ResetScanCardStorage();
        _cocktailMakingManager.ResetCocktail();
        
        _isScanningDone = false;
    }
}