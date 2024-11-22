using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public enum EEndingType
    {
        DieEnding, //사망 엔딩
        VaultEnding, //금고 엔딩
        BanishEnding //추방 엔딩
    }
    public static EndingManager Inst { get; private set; }

    private EEndingType _endingType;
    public EEndingType EndingType => _endingType;
    
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEndingType(EEndingType endingType)
    {
        _endingType = endingType;
    }
}
