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

    [SerializeField]
    private Animator _boyNPCAnimator;

    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;
    
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
        
        _playerSpriteRenderer.flipX = true;
        
        _waitChatBalloonGO.SetActive(false);
        
        _playableDirector.Play();
        _boyNPCTransform.gameObject.SetActive(true);
        _boyNPCAnimator.Play("Run");
        _boyNPCAnimator.SetBool("IsRun", true);
        _boyNPCTransform.DOMoveX(-0.73f, 1.5f)
            .OnKill(() =>
            {
                _boyNPCAnimator.SetBool("IsRun", false);
            });
        
        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);

        BarOutsideDialougeManager.Inst.StartDialogueByString(_boyNPCTransform.position, _cutSceneScripts);

        yield return new WaitUntil(() => BarOutsideDialougeManager.Inst.IsProgressed == false);
        
        _boyNPCTransform.gameObject.SetActive(false);

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
