using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DayEndingDialogueManager : MonoBehaviour
{
    public static DayEndingDialogueManager Inst { get; private set; }
    [SerializeField]
    private GameObject _chatBalloonGO;

    [SerializeField]
    private GameObject _arrowGO;
    
    [SerializeField]
    private TextMeshProUGUI _scriptText;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _typeSpeedForSecond;

    [SerializeField]
    private AudioClip _typingAudioClip;
    
    [SerializeField]
    private Sprite[][] _endingSprites;
    
    [SerializeField]
    private string[][] _dayEndingScripts;
    
    [SerializeField]
    private Image _cutSceneImage;
    
    [SerializeField]
    private Image _fadeImage;
    
    private Queue<string> _scriptsQueue = new Queue<string>();
    private Queue<Sprite> _cutSceneQueue = new Queue<Sprite>();

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    private void Awake()
    {
        Inst = this;
    }

    public void StartDialogue()
    {
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);
        _arrowGO.SetActive(true);
        
        _scriptsQueue.Clear();

        foreach (var script in _dayEndingScripts[(int)EndingManager.Inst.EndingType])
        {
            _scriptsQueue.Enqueue(script);
        }
        
        foreach (var cutSprite in _endingSprites[(int)EndingManager.Inst.EndingType])
        {
            _cutSceneQueue.Enqueue(cutSprite);
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

        _cutSceneImage.sprite = _cutSceneQueue.Dequeue();

        string script = _scriptsQueue.Dequeue();
        _currentScript = script;
        
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
        
        DayManager.Instance.ResetDayManager();
        InventoryManager.Instance().ResetInventoryData();
        InventoryManager.Instance().ResetItems();
        PlayerManager.Instance().ResetPlayerData();
        PlayerManager.Instance().IsNewItemDotActivated = false;
        
        _fadeImage.DOFade(1f, 2f)
            .OnKill(() =>
            {
                ShowCredit();
            });
    }

    public void ShowCredit()
    {
        
    }
}
