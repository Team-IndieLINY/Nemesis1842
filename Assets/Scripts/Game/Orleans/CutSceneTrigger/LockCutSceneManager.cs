using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LockCutSceneManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private List<string> _cutSceneScripts;

    [SerializeField]
    private GameObject _findPlayerChatBalloon;
    
    [SerializeField]
    private GameObject _findPlayerChatBalloon2;

    [SerializeField]
    private LockUI _lockUI;

    [SerializeField]
    private Timer _timer;
    
    private PlayableDirector _playableDirector;
    private Coroutine _timerCoroutine;
    
    public static LockCutSceneManager Inst { get; private set; }

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Inst = this;
    }

    public IEnumerator StartCutScene()
    {
        PlayerController.RestrictMovement();
        
        _playableDirector.Play();

        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);
        
        _findPlayerChatBalloon.SetActive(true);

        yield return new WaitForSeconds(1f);
        
        _findPlayerChatBalloon2.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        
        _findPlayerChatBalloon.SetActive(false);
        _findPlayerChatBalloon2.SetActive(false);

        BarOutsideDialougeManager.Inst.StartDialogueByString(_playerTransform.position, _cutSceneScripts);

        yield return new WaitUntil(() => BarOutsideDialougeManager.Inst.IsProgressed == false);
        
        EndCutScene();
    }

    public void EndCutScene()
    {
        _playableDirector.Resume();
        
        _timer.gameObject.SetActive(true);
        _timerCoroutine = StartCoroutine(_timer.StartTimer());
        PopUpUIManager.Inst.OpenUI(_lockUI);
        PlayerController.AllowMovement();
    }

    public void StopTimer()
    {
        StopCoroutine(_timerCoroutine);
    }
}
