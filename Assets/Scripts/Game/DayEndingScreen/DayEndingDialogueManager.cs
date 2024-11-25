using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DayEndingDialogueManager : MonoBehaviour
{
    [Serializable]
    public class CutSceneArray
    {
        public Sprite[] _endingSprites;
        public Sprite[] EndingSprite => _endingSprites;
        
        public string[] _dayEndingScripts;
        public string[] DayEndingScripts => _dayEndingScripts;
    }
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
    private CutSceneArray[] _cutSceneArrays;

    [SerializeField]
    private Sprite[] _missionSprites;
    
    [SerializeField]
    private Image _cutSceneImage;
    
    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private Image _creditPictureImage;

    [SerializeField]
    private Image _missionImage;
    
    private Queue<string> _scriptsQueue = new Queue<string>();
    private Queue<Sprite> _cutSceneQueue = new Queue<Sprite>();

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    private bool _isEnded;
    public bool IsEnded => _isEnded;

    private PlayableDirector _playableDirector;

    private void Awake()
    {
        Inst = this;
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        _isEnded = false;
        _missionImage.sprite = _missionSprites[(int)EndingManager.Inst.EndingType];
        _cutSceneImage.sprite = _cutSceneArrays[(int)EndingManager.Inst.EndingType].EndingSprite[0];
        _fadeImage.DOFade(0f, 2f)
            .OnKill(() =>
            {
                StartDialogue();
            });
    }

    public void StartDialogue()
    {
        _chatBalloonGO.SetActive(true);
        _arrowGO.SetActive(true);
        
        _scriptsQueue.Clear();
        
        foreach (var script in _cutSceneArrays[(int)EndingManager.Inst.EndingType].DayEndingScripts)
        {
            _scriptsQueue.Enqueue(script);
        }
        
        foreach (var cutSprite in _cutSceneArrays[(int)EndingManager.Inst.EndingType].EndingSprite)
        {
            _cutSceneQueue.Enqueue(cutSprite);
        }

        DisplayNextScript();
    }

    public void DisplayNextScript()
    {
        if (_scriptsQueue.Count == 0 && _isEnded is false)
        {
            _isEnded = true;
            EndDialogue();
            return;
        }
        
        _isProgressed = true;

        if (_cutSceneQueue.Peek() != null)
        {
            _cutSceneImage.sprite = _cutSceneQueue.Dequeue();
        }

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
                StartCoroutine(ShowCredit());
            });
    }

    private IEnumerator ShowCredit()
    {
        yield return new WaitForSeconds(1f);
        
        AudioManager.Inst.PlaySFX("dodong_1");
        _missionImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        _missionImage.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        
        if (EndingManager.Inst.EndingType == EndingManager.EEndingType.VaultEnding)
        {
            AudioManager.Inst.FadeInMusic("main_screen");
        }
        else
        {
            AudioManager.Inst.FadeInMusic("intro");
        }

        yield return new WaitForSeconds(2f);
        
        _creditPictureImage.DOFade(1f, 2f);
        _playableDirector.Play();

        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);
        
        _creditPictureImage.DOFade(0f, 2f)
            .OnKill(() =>
            {
                LoadingScreen.Instance.LoadScene("MainScreen");
            });
    }
}
