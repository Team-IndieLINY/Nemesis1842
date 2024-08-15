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
            //chat balloon이 띄어져 있을 때만 작동하게 하기
            _barDialogueManager.DisplayNextScript();
        }
    }
}
