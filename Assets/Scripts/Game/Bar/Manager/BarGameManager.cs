using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(PlayableDirector))]
public class BarGameManager : MonoBehaviour
{
    public static BarGameManager Inst { get; private set; }

    [SerializeField]
    private BarGuestDB _barGuestDB;

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private CanvasGroup _scanSelectorCanvasGroup;

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
    private Image _guestScreenBackgroundImage;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private int _cocktailPrice;

    public int CocktailPrice => _cocktailPrice;

    [SerializeField]
    private int _overloadPrice;

    public int OverloadPrice => _overloadPrice;

    [SerializeField]
    private int _cocktailMistakePrice;

    public int CocktailMistakePrice => _cocktailMistakePrice;

    [SerializeField]
    private ReceiptUI _receiptUI;

    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private SpriteRenderer _cocktailSpriteRenderer;

    [SerializeField]
    private Image _fadeImage;


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

    [SerializeField]
    private CocktailMakingScreenDialougeManager _cocktailMakingScreenDialougeManager;

    private int _stepCount;
    public int StepCount => _stepCount;

    private List<BarGuestEntity> _guestDatas;

    private List<StepEntity> _stepDatas;

    private PlayableDirector _playableDirector;

    private void Awake()
    {
        Inst = this;
        _playableDirector = GetComponent<PlayableDirector>();
        _fadeImage.color = new Color32(0, 0, 0, 255);
        AudioManager.Inst.FadeInMusic("bar");
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
        TutorialManager.Inst.ResetTutorialManager();
        _fadeImage.DOFade(0f, 3f);
        yield return new WaitForSeconds(3f);
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

            _stolenManager.SetStolenManager(guestData.guest_code);

            yield return new WaitUntil(() => _barDialogueManager.IsProgressed == false);

            for (int i = 1; i <= _stepDatas.Count; i++)
            {
                SetStep(_stepDatas[i - 1]);
                
                EnterScanPhase();

                yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);
                
                ShowScannerSelector();
                
                yield return new WaitUntil(() =>
                    ScanManager.Inst.IsScanningDone);

                EnterAlcoholPhase();
                
                yield return new WaitUntil(() => _alcoholController.IsAlcoholPhaseDone == true);
                
                EnterCocktailMakingScreen();
                
                yield return new WaitUntil(() =>
                    _playableDirector.state == PlayState.Paused);

                _cocktailMakingScreenDialougeManager.StartDialogue(_stepDatas[i - 1].cocktail_summary_text);

                if (TutorialManager.Inst.UseTutorial)
                {
                    TutorialManager.Inst.ShowTutorialByIndex(5);
                }
                
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

            _cocktailSpriteRenderer.color = new Color32(255, 255, 255, 255);

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

        _inventory.SaveInventoryData();
        _player.SaveMoney();

        DayManager.Instance.ChangeTimeType();

        AudioManager.Inst.FadeOutMusic("bar");
        _fadeImage.DOFade(1f, 2f)
            .OnKill(() =>
            {
                LoadingScreen.Instance.LoadScene("Orleans");
            });
    }

    private void EnterAlcoholPhase()
    {
        _alcoholController.ActivateAlcoholController();
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
            = _barGuestDB.BarScripts
                .Where(x => x.guest_code == guestData.guest_code &&
                            x.step_order == stepOrder)
                .ToList();

        _barDialogueManager.StartDialogue(barDialogueEntity);
    }

    private void ShowScannerSelector()
    {
        _scanSelectorGO.SetActive(true);
        _guestScreenBackgroundImage.DOFade(0.8f, 0.3f);
        _scanSelectorCanvasGroup.DOFade(1f, 0.3f);
        
        if (TutorialManager.Inst.UseTutorial)
        {
            TutorialManager.Inst.ShowTutorial();
        }
    }

    public void EnterScanPhase()
    {
        _playableDirector.playableAsset = _cocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    public void StartScan()
    {
        _scanSelectorGO.SetActive(false);
        if (TutorialManager.Inst.UseTutorial)
        {
            TutorialManager.Inst.ShowTutorial();
        }
        ScanManager.Inst.EnterScanPhase();
    }

    private void EnterCocktailMakingScreen()
    {
        _playableDirector.Resume();
    }

    public void PauseTimeline()
    {
        _playableDirector.Pause();
    }

    public void OnClickEnterCutSceneButton()
    {
        _cocktailMakingScreenDialougeManager.EndDialogue();
        _playableDirector.Resume();
    }

    public void ExitCocktailMakingScreen()
    {
        AudioManager.Inst.PlaySFX("mouse_click");
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
        int money = stepCount * _cocktailPrice - _alcoholController.SumOfUsingMachineCount * _overloadPrice -
                    _cocktailMakingManager.CocktailMistakeCount * _cocktailMistakePrice;

        _player.EarnMoney(money);
    }

    private void SetStep(StepEntity stepEntity)
    {
        _guest.StepData = stepEntity;
        _alcoholController.SetAlcoholController(stepEntity.answer_alcohol);

        ScanManager.Inst.AnswerConditionType = (ConditionScanData.EConditionType)stepEntity.condition_answer;
        
        ScanManager.Inst.AnswerLeavenType = (LiverScanData.ELeavenType)stepEntity.leaven_answer;
        ScanManager.Inst.SetLiver(stepEntity.square_leaven_count, stepEntity.circle_leaven_count,
            stepEntity.star_leaven_count, stepEntity.triangle_leaven_count);

        ScanManager.Inst.AnswerHeartbeatType = (HeartbeatScanData.EHeartbeatType)stepEntity.heartbeat_answer;
        ScanManager.Inst.SetHeartbeatScanner();


        List<CocktailRejectScriptEntity> cocktailRejectScriptEntities = _barGuestDB.CocktailRejectScripts
            .Where(x => x.guest_code == stepEntity.guest_code && x.step_order == stepEntity.step_order)
            .ToList();
        
        _cocktailMakingManager.SetStepCocktail(stepEntity.cocktail_code, cocktailRejectScriptEntities);
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
        _cocktailMakingManager.ResetTurnCocktail();
    }

    private void EnterStolenPhase()
    {
        _stolenManager.ShowStolenSelector();
    }
}