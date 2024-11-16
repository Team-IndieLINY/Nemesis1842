using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarDialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _chatBalloonGO;

    [SerializeField]
    private GameObject _tutorialChatBalloonGO;

    [SerializeField]
    private GameObject _arrowGO;

    [SerializeField]
    private GameObject _tutorialArrowGO;
    
    [SerializeField]
    private TextMeshProUGUI _characterNameText;
    
    [SerializeField]
    private TextMeshProUGUI _tutorialCharacterNameText;

    [SerializeField]
    private TextMeshProUGUI _scriptText;
    
    [SerializeField]
    private TextMeshProUGUI _tutorialScriptText;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _typeSpeedForSecond;

    [SerializeField]
    private AudioClip _typingAudioClip;

    [SerializeField]
    private Guest _guest;

    [SerializeField]
    private Animator _mouthAnimator;
    
    [SerializeField]
    private Image _tutorialBlackBackground;
    
    private Queue<BarDialogueEntity> _scriptsQueue = new Queue<BarDialogueEntity>();
    private Queue<string> _tutorialScriptsQueue = new Queue<string>();

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
    
    public void StartTutorialDialogue(List<string> tutorialScripts)
    {
        _tutorialBlackBackground.raycastTarget = true;
        _tutorialBlackBackground.DOKill();
        _tutorialBlackBackground.DOFade(0.8f, 0.4f);
        _isProgressed = true;
        
        _tutorialChatBalloonGO.SetActive(true);
        _tutorialArrowGO.SetActive(true);
        
        _tutorialScriptsQueue.Clear();

        foreach (var tutorialScript in tutorialScripts)
        {
            _tutorialScriptsQueue.Enqueue(tutorialScript);
        }
        
        DisplayNextTutorialScript();
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
    
    public void DisplayNextTutorialScript()
    {
        if (_tutorialScriptsQueue.Count == 0)
        {
            EndTutorialDialogue();
            return;
        }

        string barDialogueEntity = _tutorialScriptsQueue.Dequeue();
        _currentScript = barDialogueEntity;
        
        _tutorialCharacterNameText.text = "휘뚜룹";
        
        _typeScriptsCoroutine = StartCoroutine(TypeTutorialScripts(_currentScript));
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
    
    private IEnumerator TypeTutorialScripts(string script)
    {
        _isTyped = true;
        _tutorialScriptText.text = "";

        foreach (var letter in script.ToCharArray())
        {
            AudioManager.Inst.PlaySFX("type");
            _tutorialScriptText.text += letter;
            yield return new WaitForSeconds(_typeSpeedForSecond);
        }
        
        _isTyped = false;
    }

    public void SkipTypeScripts()
    {
        StopCoroutine(_typeScriptsCoroutine);

        _scriptText.text = _currentScript;

        _mouthAnimator.enabled = false;
        _isTyped = false;
    }
    
    public void SkipTypeTutorialScripts()
    {
        StopCoroutine(_typeScriptsCoroutine);

        _tutorialScriptText.text = _currentScript;
        _isTyped = false;
    }
    
    public void EndDialogue()
    {
        _isProgressed = false;
        _chatBalloonGO.SetActive(false);
    }
    
    public void EndTutorialDialogue()
    {
        _tutorialBlackBackground.DOKill();
        _tutorialBlackBackground.DOFade(0f, 0.4f)
            .OnKill(() =>
            {
                _tutorialBlackBackground.raycastTarget = false;
            });
        _isProgressed = false;
        _tutorialChatBalloonGO.SetActive(false);
    }
}
