using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private static DayManager _instance = null;
    
    [SerializeField,Range(1,20)]
    private int day = 1;
    public int Day => day;

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
}