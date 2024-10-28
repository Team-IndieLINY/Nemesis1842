using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BarOutsideTutorialManager : MonoBehaviour
{
    [SerializeField]
    private bool _useTutorial;

    [SerializeField]
    private CanvasGroup _tutorialImageCanvasGroup;

    private void Awake()
    {
        _tutorialImageCanvasGroup.alpha = 0f;
        if (_useTutorial)
        {
            _tutorialImageCanvasGroup.DOFade(1f, 0.7f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _useTutorial)
        {
            _tutorialImageCanvasGroup.DOFade(0f, 0.4f);
        }
    }
}
