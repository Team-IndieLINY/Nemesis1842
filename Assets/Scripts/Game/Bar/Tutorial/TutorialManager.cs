using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Inst { get; private set; }
    
    [Header("튜토리얼")]
    [SerializeField]
    private bool _useTutorial;

    public bool UseTutorial => _useTutorial;

    [SerializeField]
    private List<GameObject> _tutorialUIs;

    private List<CanvasGroup> _tutorialCanvasGroups = new();

    private bool _isTutorialOn;

    private void Awake()
    {
        Inst = this;

        for (int i = 0; i < _tutorialUIs.Count; i++)
        {
            _tutorialCanvasGroups.Add(_tutorialUIs[i].GetComponent<CanvasGroup>());
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isTutorialOn == true)
        {
            for (int i=0;i<_tutorialCanvasGroups.Count;i++)
            {
                if (_tutorialCanvasGroups[i].alpha >= 1f && _tutorialCanvasGroups[i].gameObject.activeSelf == true)
                {
                    HideTutorial(i);
                }
            }
        }
    }

    public void ShowTutorial(int index)
    {
        _isTutorialOn = true;
        _tutorialUIs[index].SetActive(true);
        _tutorialCanvasGroups[index].DOFade(1f, 0.8f);
    }
    
    public void HideTutorial(int index)
    {
        _isTutorialOn = false;
        _tutorialCanvasGroups[index].DOFade(0f, 0.3f)
            .OnKill(() =>
            {
                _tutorialUIs[index].SetActive(false);
            });
    }
}