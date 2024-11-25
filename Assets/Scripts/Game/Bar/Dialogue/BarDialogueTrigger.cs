using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BarDialogueManager))]
public class BarDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _chatBalloonGO;

    [SerializeField]
    private GameObject _tutorialChatBalloonGO;
    
    private BarDialogueManager _barDialogueManager;

    private void Awake()
    {
        _barDialogueManager = GetComponent<BarDialogueManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if (_barDialogueManager.IsTyped is true)
            {
                if (_chatBalloonGO.activeSelf == true)
                {
                    _barDialogueManager.SkipTypeScripts();
                }

                if (_tutorialChatBalloonGO.activeSelf == true)
                {
                    _barDialogueManager.SkipTypeTutorialScripts();
                }
            }
            else
            {
                if (_chatBalloonGO.activeSelf == true)
                {
                    _barDialogueManager.DisplayNextScript();
                }

                if (_tutorialChatBalloonGO.activeSelf == true)
                {
                    _barDialogueManager.DisplayNextTutorialScript();
                }
            }
        }
    }
}
