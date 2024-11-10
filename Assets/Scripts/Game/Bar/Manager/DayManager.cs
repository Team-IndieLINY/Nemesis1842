using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    private static DayManager _instance = null;
    
    [SerializeField,Range(0,20)]
    private int day = 1;
    public int Day => day;
    
    [SerializeField]
    private NPCData.ETimeType _timeType = NPCData.ETimeType.Evening;
    public NPCData.ETimeType TimeType =>_timeType;

    [SerializeField]
    private int _monthlyRent;

    public int MonthlyRent => _monthlyRent;

    void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static DayManager Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    public void IncreaseDay()
    {
        day++;
        ChangeTimeType();
    }

    public void ChangeTimeType()
    {
        if (_timeType == NPCData.ETimeType.Evening)
        {
            _timeType = NPCData.ETimeType.Dawn;
        }
        else if (_timeType == NPCData.ETimeType.Dawn)
        {
            _timeType = NPCData.ETimeType.Evening;
        }
    }

    public void ResetDayManager()
    {
        day = 1;
        _timeType = NPCData.ETimeType.Evening;
        _monthlyRent = 300;
    }
}