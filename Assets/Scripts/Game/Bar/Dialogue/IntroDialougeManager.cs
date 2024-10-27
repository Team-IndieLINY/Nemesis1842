using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroDialougeManager : MonoBehaviour
{
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
    private Sprite[] _cutSceneSprites;

    [SerializeField]
    private string[] _cutSceneScripts;

    [SerializeField]
    private Image _cutSceneImage;
    
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
        StartDialogue();
    }

    public void StartDialogue()
    {
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);
        _arrowGO.SetActive(true);
        
        _scriptsQueue.Clear();

        foreach (var script in _cutSceneScripts)
        {
            _scriptsQueue.Enqueue(script);
        }

        foreach (var cutSceneSprite in _cutSceneSprites)
        {
            _cutSceneQueue.Enqueue(cutSceneSprite);
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
            AudioManager.Instance.PlaySFX(_typingAudioClip);
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
        SceneManager.LoadScene("Scenes/Game/Orleans");
    }

    public void SkipIntro()
    {
        SkipTypeScripts();
        EndDialogue();
    }
}
