using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEndingDialogueTrigger : MonoBehaviour
{
    private DayEndingDialogueManager _dayEndingDialogueManager;

    private void Awake()
    {
        _dayEndingDialogueManager = GetComponent<DayEndingDialogueManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dayEndingDialogueManager.IsTyped is true)
            {
                _dayEndingDialogueManager.SkipTypeScripts();
            }
            else
            {
                _dayEndingDialogueManager.DisplayNextScript();
            }
        }
    }
}
