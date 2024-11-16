using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutSceneManager : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    public static CutSceneManager Inst { get; private set; }

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Inst = this;
    }

    public void StartCutScene()
    {
        _playableDirector.Play();
        PlayerController.RestrictMovement();
    }

    public void EndCutScene()
    {
        _playableDirector.Resume();
        PlayerController.AllowMovement();
    }
}
