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
            if (gameObject.name == "ChatBalloonImage")
            {
                _barDialogueManager.SkipTypeScripts();
            }

            if (gameObject.name == "TutorialChatBalloonImage")
            {
                _barDialogueManager.SkipTypeTutorialScripts();
            }
        }
        else
        {
            if (gameObject.name == "ChatBalloonImage")
            {
                _barDialogueManager.DisplayNextScript();
            }

            if (gameObject.name == "TutorialChatBalloonImage")
            {
                _barDialogueManager.DisplayNextTutorialScript();
            }
        }
    }
}
