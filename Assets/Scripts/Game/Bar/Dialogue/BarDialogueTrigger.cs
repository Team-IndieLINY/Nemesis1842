using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BarDialogueManager))]
public class BarDialogueTrigger : MonoBehaviour
{
    private BarDialogueManager _barDialogueManager;

    private void Awake()
    {
        _barDialogueManager = GetComponent<BarDialogueManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_barDialogueManager.IsTyped is true)
            {
                _barDialogueManager.SkipTypeScripts();
            }
            else
            {
                _barDialogueManager.DisplayNextScript();
            }
        }
    }
}
