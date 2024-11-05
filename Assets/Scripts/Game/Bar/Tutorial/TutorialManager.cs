using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Inst { get; private set; }
    
    [Header("튜토리얼")]
    [SerializeField]
    private bool _useTutorial;

    public bool UseTutorial => _useTutorial;

    [SerializeField]
    private List<Image> _tutorialUIs;

    private bool _isTutorialOn;
    public bool IsTutorialOn => _isTutorialOn;

    private bool _isShowing;
    public bool IsShowing => _isShowing;
    
    private int _tutorialUIIndex;
    public int TutorialUIIndex => _tutorialUIIndex;

    private void Awake()
    {
        _tutorialUIIndex = 0;
        Inst = this;
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isTutorialOn == true)
        {
            _isTutorialOn = false;
            HideTutorial();
        }
    }

    public void ShowTutorial()
    {
        _isShowing = true;
        _tutorialUIs[_tutorialUIIndex].gameObject.SetActive(true);
        _tutorialUIs[_tutorialUIIndex].DOFade(1f, 0.4f)
            .OnKill(() =>
            {
                _isTutorialOn = true;
            });
    }

    public void ShowTutorialByIndex(int index)
    {
        _isShowing = true;
        _tutorialUIIndex = index;
        _tutorialUIs[_tutorialUIIndex].gameObject.SetActive(true);
        _tutorialUIs[_tutorialUIIndex].DOFade(1f, 0.4f)
            .OnKill(() =>
            {
                _isTutorialOn = true;
            });
    }
    
    public void HideTutorial()
    {
        _tutorialUIs[_tutorialUIIndex].DOKill();
        _tutorialUIs[_tutorialUIIndex].DOFade(0f,0.4f)
            .OnKill(() =>
            {
                _isShowing = false;
                _tutorialUIs[_tutorialUIIndex].gameObject.SetActive(false);
                _tutorialUIIndex++;

                if (_tutorialUIIndex == 2)
                {
                    ShowTutorial();
                }

                if (_tutorialUIIndex == 6)
                {
                    ShowTutorial();
                }

                if (_tutorialUIIndex == 11)
                {
                    ShowTutorial();
                }

                if (_tutorialUIIndex == 12)
                {
                    ShowTutorial();
                }
                
                if (_tutorialUIIndex == _tutorialUIs.Count)
                {
                    _useTutorial = false;
                }
            });
    }

    public void ResetTutorialManager()
    {
        _isShowing = false;
        _isTutorialOn = false;
        _tutorialUIIndex = 0;
    }
}