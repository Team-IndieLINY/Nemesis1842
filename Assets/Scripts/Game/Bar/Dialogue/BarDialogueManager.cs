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
    private GameObject _makeCocktailButtonGO;

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
    private AudioSource _audioSource;
    
    private Queue<BarDialogueEntity> _scriptsQueue = new Queue<BarDialogueEntity>();

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    public void StartDialogue(List<BarDialogueEntity> barDialogueEntities)
    {
        _audioSource.clip = _typingAudioClip;
        
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
            if (_makeCocktailButtonGO.activeSelf is false) //해당 버튼이 켜져있으면, 칵테일 제조 후 대사라고 판단
            {
                EndDialogue();
            }
            return;
        }
        
        if (_scriptsQueue.Count == 1 && _scriptsQueue.Peek().script_type == 0) //마지막 대사이고 시작 스크립트이면 칵테일 제조 버튼 활성화
        {
            _arrowGO.SetActive(false);
            _makeCocktailButtonGO.SetActive(true);
        }

        BarDialogueEntity barDialogueEntity = _scriptsQueue.Dequeue();
        _currentScript = barDialogueEntity.script;
        
        _characterNameText.text = barDialogueEntity.character_name;
        
        _typeScriptsCoroutine = StartCoroutine(TypeScripts(_currentScript));
    }
    
    private IEnumerator TypeScripts(string script)
    {
        _isTyped = true;
        _scriptText.text = "";

        foreach (var letter in script.ToCharArray())
        {
            _audioSource.Play();
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
        //아래 두 줄 순서 바뀌면 안됨 (두 줄 순서가 바뀌면 나중에 대사 시작 시 버튼이 안뜸)
        _makeCocktailButtonGO.SetActive(false);
        _chatBalloonGO.SetActive(false);
    }
}
