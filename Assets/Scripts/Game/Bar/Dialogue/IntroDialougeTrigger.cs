using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IntroDialougeManager))]
public class IntroDialougeTrigger : MonoBehaviour
{
    private IntroDialougeManager _introDialougeManager;

    private void Awake()
    {
        _introDialougeManager = GetComponent<IntroDialougeManager>();
    }

    void Update()
    {
        if (_introDialougeManager.IsEnded)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            if (_introDialougeManager.IsTyped is true)
            {
                _introDialougeManager.SkipTypeScripts();
            }
            else
            {
                _introDialougeManager.DisplayNextScript();
            }
        }
    }
}
