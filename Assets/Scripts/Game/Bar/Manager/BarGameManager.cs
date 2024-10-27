using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

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

    [SerializeField]
    private Image _guestScreenLeftBackgroundImage;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private int _cocktailPrice;

    public int CocktailPrice => _cocktailPrice;

    [SerializeField]
    private int _overloadPrice;

    public int OverloadPrice => _overloadPrice;

    [SerializeField]
    private ReceiptUI _receiptUI;
    
    [Header("타임라인")]
    [SerializeField]
    private TimelineAsset _appearGuestTimeline;

    [SerializeField]
    private TimelineAsset _cocktailMakingScreenTimeline;

    [SerializeField]
    private TimelineAsset _appearCocktailTimeline;

    [SerializeField]
    private TimelineAsset _disappearGuestTimeline;

    [Header("Manager")]
    [SerializeField]
    private BarDialogueManager _barDialogueManager;

    [SerializeField]
    private GuideLineManager _guideLineManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private StolenManager _stolenManager;

    private int _stepCount;
    public int StepCount => _stepCount;

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
    private void LoadGuestData()
    {
        _guestDatas = _barGuestDB.Guests
            .Where(x => x.day == DayManager.Instance.Day)
            .OrderBy(x => x.order)
            .ToList();
    }

    private IEnumerator RoutineBar()
    {
        yield return new WaitForSeconds(2f);
        foreach (var guestData in _guestDatas)
        {
            _stepDatas = _barGuestDB.Steps
                .Where(x => x.guest_code == guestData.guest_code)
                .ToList();

            _stepCount = _stepDatas.Count;
            
            AppearGuest(guestData);

            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _appearGuestTimeline
                && _playableDirector.state == PlayState.Paused);
            
            ShowScripts(0, guestData);
            
            yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);
            
            for (int i = 1; i <= _stepDatas.Count; i++)
            {
                SetStep(_stepDatas[i - 1]);
                
                ShowScannerSelector();

                yield return new WaitUntil(() =>
                    ScanManager.Inst.IsScanningDone);

                EnterCocktailMakingScreen();

                yield return new WaitUntil(() =>
                    _playableDirector.state == PlayState.Paused && _playableDirector.time == 0f);

                AppearCocktail();

                yield return new WaitUntil(() =>
                    _playableDirector.state == PlayState.Paused &&
                    _playableDirector.playableAsset == _appearCocktailTimeline);

                ShowScripts(i, guestData);
                
                yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);
                
                ResetStep();
            }
            
            _guest.DrunkGuest();

            yield return new WaitForSeconds(1f);

            EnterStolenPhase();

            yield return new WaitUntil(() => _stolenManager.IsStolenDone);
            
            DisappearGuest();
            
            yield return new WaitUntil(() =>
                _playableDirector.state == PlayState.Paused &&
                _playableDirector.playableAsset == _disappearGuestTimeline);

            ShowReceiptUI();

            yield return new WaitUntil(() =>
                _receiptUI.IsShown == false);
            
            EarnMoney(_stepDatas.Count);

            ResetTurn();
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
        _guestScreenLeftBackgroundImage.DOColor(new Color32(255, 255, 255, 255), 0.3f);
        _scanSelectorGO.SetActive(true);
    }

    public void EnterScanPhase()
    {
        _scanSelectorGO.SetActive(false);
        ScanManager.Inst.EnterScanPhase();
    }

    private void EnterCocktailMakingScreen()
    {
        _playableDirector.playableAsset = _cocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    public void PauseTimeline()
    {
        _playableDirector.Pause();
    }

    public void OnClickEnterAlcoholPhaseButton()
    {
        _playableDirector.Resume();
    }
    
    public void OnClickEnterCutSceneButton()
    {
        _playableDirector.Resume();
    }

    public void ExitCocktailMakingScreen()
    {
        _playableDirector.Resume();
    }
    
    private void AppearCocktail()
    {
        _playableDirector.playableAsset = _appearCocktailTimeline;
        _playableDirector.Play();
    }

    private void DisappearGuest()
    {
        _playableDirector.playableAsset = _disappearGuestTimeline;
        _playableDirector.Play();
    }

    private void ShowReceiptUI()
    {
        _receiptUI.UpdateReceiptUI();
        _receiptUI.ShowReceipt();
    }

    private void EarnMoney(int stepCount)
    {
        int money = stepCount * _cocktailPrice - _alcoholController.SumOfOverloadCount * _overloadPrice;
        
        _player.EarnMoney(money);
    }

    private void SetStep(StepEntity stepEntity)
    {
        _guest.StepData = stepEntity;
        _cocktailRecipeUI.SetCocktailSummaryText(stepEntity.cocktail_summary_text);
        _alcoholController.SetAlcoholController(stepEntity.max_answer_alcohol,
            stepEntity.min_answer_alcohol, stepEntity.alcohol_control_attempt);

        ScanManager.Inst.AnswerConditionType = (ConditionScanData.EConditionType)stepEntity.condition_answer;
        ScanManager.Inst.AnswerLeavenType = (LiverScanData.ELeavenType)stepEntity.leaven_answer;
        ScanManager.Inst.SetLiver(stepEntity.square_leaven_count, stepEntity.circle_leaven_count,
            stepEntity.star_leaven_count, stepEntity.triangle_leaven_count);

        _stolenManager.SetStolenManager(stepEntity.guest_code);
    }

    private void ResetStep()
    {
        _openCocktailRecipeButtonGO.SetActive(true);
        _enterAlcoholPhaseButtonGO.SetActive(true);
        _cocktailMakingManager.ResetStepCocktail();
        ScanManager.Inst.ResetStepScanManager();
        _alcoholController.ResetStepAlcoholController();
        _guideLineManager.ResetStepGuideLineInfos();
    }

    private void ResetTurn()
    {
        _stolenManager.ResetTurnStolenManager();
        _alcoholController.ResetTurnAlcoholController();
        _guest.ResetTurnGuest();
    }

    private void EnterStolenPhase()
    {
        
        _stolenManager.ShowStolenSelector();
    }
}