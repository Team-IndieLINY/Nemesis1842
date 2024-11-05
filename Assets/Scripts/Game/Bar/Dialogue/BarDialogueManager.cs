using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarDialogueManager : MonoBehaviour
{
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

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private Animator _mouthAnimator;
    
    private Queue<BarDialogueEntity> _scriptsQueue = new Queue<BarDialogueEntity>();

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    private void Awake()
    {
        _mouthAnimator.enabled = false;
    }

    public void StartDialogue(List<BarDialogueEntity> barDialogueEntities)
    {
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

        BarDialogueEntity barDialogueEntity = _scriptsQueue.Dequeue();
        _currentScript = barDialogueEntity.script;
        _guest.SetCharacterSprite(barDialogueEntity.character_sprite_code);
        
        _characterNameText.text = barDialogueEntity.character_name;
        _mouthAnimator.enabled = true;
        _mouthAnimator.SetBool(barDialogueEntity.character_sprite_code, true);
        
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
        
        _mouthAnimator.enabled = false;
        _isTyped = false;
    }

    public void SkipTypeScripts()
    {
        StopCoroutine(_typeScriptsCoroutine);

        _scriptText.text = _currentScript;

        _mouthAnimator.enabled = false;
        _isTyped = false;
    }
    
    public void EndDialogue()
    {
        _isProgressed = false;
        _chatBalloonGO.SetActive(false);
    }
}
