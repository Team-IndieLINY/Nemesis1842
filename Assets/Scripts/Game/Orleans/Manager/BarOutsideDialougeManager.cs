using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarOutsideDialougeManager : MonoBehaviour
{
    public static BarOutsideDialougeManager Inst { get; private set; }
    
    [SerializeField]
    private GameObject _chatBalloonGO;

    [SerializeField]
    private GameObject _arrowGO;
    
    [SerializeField]
    private TextMeshProUGUI _characterNameText;

    [SerializeField]
    private TextMeshProUGUI _scriptText;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _typeSpeedForSecond;

    [SerializeField]
    private AudioClip _typingAudioClip;
    
    private Queue<NPCScriptEntity> _scriptsQueue = new Queue<NPCScriptEntity>();

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    private void Awake()
    {
        Inst = this;
        _chatBalloonGO.SetActive(false);
    }
    
    public void StartDialogue(Vector3 characterPosition, List<NPCScriptEntity> barDialogueEntities)
    {
        _chatBalloonGO.transform.position = new Vector3(characterPosition.x, characterPosition.y + 1.7f, 0);
        
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);
        _arrowGO.SetActive(true);
        
        _scriptsQueue.Clear();
        
        foreach (var barDialogueEntity in barDialogueEntities)
        {
            _scriptsQueue.Enqueue(barDialogueEntity);
        }

        DisplayNextScript();
    }

    public void DisplayNextScript()
    {
        if (_scriptsQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        NPCScriptEntity barDialogueEntity = _scriptsQueue.Dequeue();
        _currentScript = barDialogueEntity.script;
        
        _typeScriptsCoroutine = StartCoroutine(TypeScripts(_currentScript));
    }
    
    private IEnumerator TypeScripts(string script)
    {
        _isTyped = true;
        _scriptText.text = "";

        foreach (var letter in script.ToCharArray())
        {
            AudioManager.Inst.PlaySFX("type");
            _scriptText.text += letter;
            yield return new WaitForSeconds(_typeSpeedForSecond);
        }

        _isTyped = false;
    }

    public void SkipTypeScripts()
    {
        StopCoroutine(_typeScriptsCoroutine);

        _scriptText.text = _currentScript;

        _isTyped = false;
    }
    
    public void EndDialogue()
    {
        _isProgressed = false;
        _chatBalloonGO.SetActive(false);
    }
}
