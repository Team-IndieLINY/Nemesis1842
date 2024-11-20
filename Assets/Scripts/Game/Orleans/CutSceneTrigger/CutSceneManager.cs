using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(PlayableDirector))]
public class CutSceneManager : MonoBehaviour
{
    [SerializeField]
    private Transform _boyNPCTransform;

    [SerializeField]
    private List<string> _cutSceneScripts;

    [SerializeField]
    private GameObject _waitChatBalloonGO;

    [SerializeField]
    private Image _fadeImage;
    
    private PlayableDirector _playableDirector;
    public static CutSceneManager Inst { get; private set; }

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Inst = this;
    }

    public IEnumerator StartCutScene()
    {
        PlayerController.RestrictMovement();
        
        _waitChatBalloonGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        
        _waitChatBalloonGO.SetActive(false);
        
        _playableDirector.Play();

        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);

        BarOutsideDialougeManager.Inst.StartDialogueByString(_boyNPCTransform.position, _cutSceneScripts);

        yield return new WaitUntil(() => BarOutsideDialougeManager.Inst.IsProgressed == false);

        EndCutScene();
    }

    public void EndCutScene()
    {
        _playableDirector.Resume();
        
        AudioManager.Inst.FadeOutMusic("evening");

        _fadeImage.DOFade(1f, 1f)
            .OnKill(() =>
            {
                PlayerController.AllowMovement();
                LoadingScreen.Instance.LoadScene("Bar");
            });

    }
}
