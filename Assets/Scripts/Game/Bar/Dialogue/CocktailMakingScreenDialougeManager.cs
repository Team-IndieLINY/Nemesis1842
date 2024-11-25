using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CocktailMakingScreenDialougeManager : MonoBehaviour
{
    public static CocktailMakingScreenDialougeManager Inst { get; private set; }
    
    [SerializeField]
    private GameObject _chatBalloonGO;

    [SerializeField]
    private TextMeshProUGUI _scriptText;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _typeSpeedForSecond;

    [SerializeField]
    private AudioClip _typingAudioClip;

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private void Awake()
    {
        Inst = this;
    }

    public void StartDialogue(string script)
    {
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);

        StartCoroutine(TypeScripts(script));
    }
    
    private IEnumerator TypeScripts(string script)
    {
        _isTyped = true;
        _scriptText.text = "";
        
        bool isImportant = false;

        foreach (var letter in script.ToCharArray())
        {
            AudioManager.Inst.PlaySFX("type");
            
            if (letter == '<')
            {
                isImportant = true;
                continue;
            }
            else if (letter == '>')
            {
                isImportant = false;
                continue;
            }
            
            if (isImportant is false)
            {
                _scriptText.text += letter;
            }
            else
            {
                _scriptText.text += "<color #c89e38>" + letter + "</color>";
            }
            
            yield return new WaitForSeconds(_typeSpeedForSecond);
        }

        _isTyped = false;
    }
    
    public void EndDialogue()
    {
        _isProgressed = false;
        _chatBalloonGO.SetActive(false);
    }
}
