using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLineManager : MonoBehaviour
{
    [SerializeField]
    private ConditionGuideLineInfo[] _conditionGuideLineInfos;

    [SerializeField]
    private LiverGuideLineInfo[] _liverGuideLineInfos;

    [SerializeField]
    private HeartbeatGuideLineInfo[] _heartbeatGuideLineInfos;

    public void ResetStepGuideLineInfos()
    {
        foreach (var conditionGuideLineInfo in _conditionGuideLineInfos)
        {
            conditionGuideLineInfo.ResetGuideLineInfo();
        }
        
        foreach (var liverGuideLineInfo in _liverGuideLineInfos)
        {
            liverGuideLineInfo.ResetGuideLineInfo();
        }

        foreach (var heartbeatGuideLineInfo in _heartbeatGuideLineInfos)
        {
            heartbeatGuideLineInfo.ResetGuideLineInfo();
        }
    }

}
