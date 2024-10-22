using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatBalloonHandler : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private BarDialogueManager _barDialogueManager;
    
    public void OnPointerClick(PointerEventData eventData)
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
