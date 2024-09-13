using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(PlayableDirector))]
public class GuestManager : MonoBehaviour
{
    private List<BarGuestEntity> _guestDatas;
    private List<CharacterData> _characterDatas;
    private List<BarDialogueEntity> _startScriptDatas;
    private List<BarDialogueEntity> _endScriptDatas;
    
    private PlayableDirector _playableDirector;
    
    [SerializeField]
    private BarGuestDB _barGuestDB;

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private Button _doneButton;

    [SerializeField]
    private Receipt _receipt;

    [SerializeField]
    private Player _player;
    
    [Header("Manager")]
    [SerializeField]
    private BarDialogueManager _barDialogueManager;

    [SerializeField]
    private ScanManager _scanManager;

    [SerializeField]
    private CocktailMachineManager _cocktailMachineManager;

    [SerializeField]
    private CocktailEvaluationManager _cocktailEvaluationManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private DatabaseManager _databaseManager;

    [SerializeField]
    private CocktailManager _cocktailManager;
    
    
    [Header("Timeline")]
    [SerializeField]
    private TimelineAsset _appearGuestTimeline;

    [SerializeField]
    private TimelineAsset _enterCocktailMakingScreenTimeline;
    
    [SerializeField]
    private TimelineAsset _exitCocktailMakingScreenTimeline;

    [SerializeField]
    private TimelineAsset _disappearGuestTimeline;

    [SerializeField]
    private TimelineAsset _printReceiptTimeline;

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
            .Where(x=> x.day == DayManager.Instance.Day)
            .OrderBy(x => x.order)
            .ToList();
        

        _characterDatas = Resources.LoadAll<CharacterData>("GameData/CharacterData").ToList();
    }

    private IEnumerator RoutineBar()
    {
        foreach (var guestData in _guestDatas)
        {
            _cocktailMachineManager.ResetCocktailMachine();
            
            List<CharacterData> characterData = _characterDatas
                .Where(x => x.CharacterCode == guestData.character_code)
                .ToList();
            _guest.SetGuest(characterData[0]);
            
            _startScriptDatas = _barGuestDB.Scripts
                .Where(x => x.guest_code == guestData.guest_code && x.script_type == 0)
                .ToList();
            _endScriptDatas = _barGuestDB.Scripts
                .Where(x => x.guest_code == guestData.guest_code && x.script_type == 1)
                .ToList();

            _scanManager.SetScanData(
                _barGuestDB.CocktailProblems.Where(x => x.guest_code == guestData.guest_code).ToList());

            _cocktailEvaluationManager.SetRequiringCocktailData(
                _barGuestDB.CocktailProblems.Where(x => x.guest_code == guestData.guest_code).ToList());
            
            AppearGuest();

            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _appearGuestTimeline
                && _playableDirector.state == PlayState.Paused);

            ShowStartScripts();
            
            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _exitCocktailMakingScreenTimeline
                && _playableDirector.state == PlayState.Paused);
            
            _cocktailManager.AppearCocktail();

            yield return new WaitUntil(() => _cocktailManager.IsAppear);
            
            ShowEndScripts();

            yield return new WaitUntil(() =>
                _barDialogueManager.IsProgressed is false);
            
            DisappearGuest();
            _cocktailManager.DisappearCocktail();

            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _disappearGuestTimeline
                && _playableDirector.state == PlayState.Paused);
            
            PayCocktailCost();

            yield return new WaitUntil(() =>
                _playableDirector.playableAsset == _printReceiptTimeline
                && _playableDirector.state == PlayState.Paused);

            _cocktailMakingManager.ResetCocktail();
            _scanManager.ResetScanner();
            _databaseManager.HomeButton();
        }
    }
    
    private void AppearGuest()
    {
        _playableDirector.playableAsset = _appearGuestTimeline;
        _playableDirector.Play();
        
    }

    private void ShowStartScripts()
    {
        _barDialogueManager.StartDialogue(_startScriptDatas);
    }

    public void EnterCocktailMakingScreen()
    {
        _cocktailMakingManager.ActivateDoneAndResetButton();
        
        _playableDirector.playableAsset = _enterCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }

    public void ExitCocktailMakingScreen()
    {
        _cocktailMakingManager.InActivateDoneAndResetButton();
        
        _playableDirector.playableAsset = _exitCocktailMakingScreenTimeline;
        _playableDirector.Play();
    }
    
    private void ShowEndScripts()
    {
        _barDialogueManager.StartDialogue(_endScriptDatas);
    }
    
    private void DisappearGuest()
    {
        _playableDirector.playableAsset = _disappearGuestTimeline;
        _playableDirector.Play();
    }

    private void PayCocktailCost()
    {
        _cocktailEvaluationManager.CalculateCocktailCost();
        _receipt.SetReceiptText();

        _playableDirector.playableAsset = _printReceiptTimeline;
        _playableDirector.Play();
        _player.EarnMoney(_cocktailEvaluationManager.SumOfCocktailCost);
    }
}