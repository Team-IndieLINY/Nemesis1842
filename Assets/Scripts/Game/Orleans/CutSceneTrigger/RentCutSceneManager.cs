using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class RentCutSceneManager : MonoBehaviour
{
    [SerializeField]
    private Transform _gangNPCTransform;

    [SerializeField]
    private List<string> _cutSceneScripts;
    
    [SerializeField]
    private List<string> _canGiveMonthlyRentSceneScripts;
    
    [SerializeField]
    private List<string> _cantGiveMonthlyRentSceneScripts;

    [SerializeField]
    private PlayerOutside _playerOutside;

    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private GameObject _gangGroupGO;
    
    private PlayableDirector _playableDirector;
    public static RentCutSceneManager Inst { get; private set; }

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Inst = this;
    }

    private void Start()
    {
        if (DayManager.Instance.Day == 3 && DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
        {
            _gangGroupGO.SetActive(true);
            StartCoroutine(StartCutScene());
        }
    }

    private IEnumerator StartCutScene()
    {
        PlayerController.RestrictMovement();
        
        _playableDirector.Play();

        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);

        BarOutsideDialougeManager.Inst.StartDialogueByString(_gangNPCTransform.position, _cutSceneScripts);

        yield return new WaitUntil(() => BarOutsideDialougeManager.Inst.IsProgressed == false);

        yield return new WaitForSeconds(0.5f);

        if (_playerOutside.Money >= DayManager.Instance.MonthlyRent)
        {
            BarOutsideDialougeManager.Inst.StartDialogueByString(_gangNPCTransform.position, _canGiveMonthlyRentSceneScripts);
        }
        else
        {
            BarOutsideDialougeManager.Inst.StartDialogueByString(_gangNPCTransform.position, _cantGiveMonthlyRentSceneScripts);
        }

        yield return new WaitUntil(() => BarOutsideDialougeManager.Inst.IsProgressed == false);
        
        EndCutScene();
    }

    private void EndCutScene()
    {
        _playableDirector.Resume();
        
        if (_playerOutside.Money >= DayManager.Instance.MonthlyRent)
        {
            PlayerController.AllowMovement();
            _playerOutside.EarnMoney(-DayManager.Instance.MonthlyRent);
        }
        else
        {
            //월세 못 내는 엔딩
            AudioManager.Inst.FadeOutMusic("dawn");
            _fadeImage.DOFade(1f, 1f)
                .OnKill(() =>
                {
                    PlayerController.AllowMovement();
                    LoadingScreen.Instance.LoadScene("DayEndingScene");
                });
        }
    }
}
