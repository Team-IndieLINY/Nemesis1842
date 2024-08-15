using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(PlayableDirector))]
public class ScanManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _scanResultPaperGOs;

    [SerializeField]
    private TimelineAsset[] _scanResultPrintTimelines;

    [SerializeField]
    private Transform _scanResultSpaceTransform;
    [FormerlySerializedAs("_scanResultPaperSpawnPoint")]
    [SerializeField]
    private RectTransform _scanResultPaperPrintStartPoint;
    
    private PlayableDirector _playableDirector;
    
    
    
    public enum ScanType
    {
        ScriptScan = 0,
        HeartBeatScan,
        BehaviourScan,
        WearingScan
    }

    private Dictionary<ScanType, bool> _scanTypeByHasScan
        = new()
        {
            { ScanType.ScriptScan, false },
            { ScanType.HeartBeatScan, false },
            { ScanType.BehaviourScan, false },
            { ScanType.WearingScan, false }
        };

    private Dictionary<ScanType, string> _scanTypeByScanResultScript
        = new()
        {
            { ScanType.ScriptScan, "" },
            { ScanType.HeartBeatScan, "" },
            { ScanType.BehaviourScan, "" },
            { ScanType.WearingScan, "" }
        };

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void SetScanData(List<BarCocktailProblemEntity> barCocktailProblemEntities)
    {
        foreach (var barCocktailProblemEntity in barCocktailProblemEntities)
        {
            if (!Enum.IsDefined(typeof(ScanType), barCocktailProblemEntity.scan_type))
                throw new ArgumentOutOfRangeException();
            
            _scanTypeByHasScan[(ScanType)barCocktailProblemEntity.scan_type] = true;
            _scanTypeByScanResultScript[(ScanType)barCocktailProblemEntity.scan_type] =
                barCocktailProblemEntity.scan_script;
        }
    }

    //스캔 결과지들을 전부 삭제하고 현재 손님이 제시한 문제의 스캔 요소
    public void ResetScanner()
    {
        _playableDirector.Stop();
        
        int paperCount = _scanResultSpaceTransform.childCount;

        for (int i = 0; i < paperCount; i++)
        {
            _scanResultPaperGOs[i].GetComponent<RectTransform>().anchoredPosition
                = _scanResultPaperPrintStartPoint.anchoredPosition;
            
            _scanResultPaperGOs[i].GetComponent<ScanResultPaper>().ResetResultPaper();
        }

        List<ScanType> scanTypes = _scanTypeByHasScan.Keys.ToList();
        
        foreach (var scanType in scanTypes)
        {
            _scanTypeByHasScan[scanType] = false;
            _scanTypeByScanResultScript[scanType] = "";
        }
    }

    public void OnClickScriptScanButton()
    {
        if (_scanTypeByHasScan[ScanType.ScriptScan] is false || _playableDirector.state == PlayState.Playing)
        {
            return;
        }

        _scanTypeByHasScan[ScanType.ScriptScan] = false;
        
        _scanResultPaperGOs[(int)ScanType.ScriptScan].GetComponent<ScanResultPaper>()
            .SetScanResultPaper(_scanTypeByScanResultScript[ScanType.ScriptScan]);

        StartCoroutine(AnimatePrintScanResultPaper(ScanType.ScriptScan));
    }

    public void OnClickHeartBeatScanButton()
    {
        if (_scanTypeByHasScan[ScanType.HeartBeatScan] is false || _playableDirector.state == PlayState.Playing)
        {
            return;
        }

        _scanTypeByHasScan[ScanType.HeartBeatScan] = false;
        
        _scanResultPaperGOs[(int)ScanType.HeartBeatScan].GetComponent<ScanResultPaper>()
            .SetScanResultPaper(_scanTypeByScanResultScript[ScanType.HeartBeatScan]);

        StartCoroutine(AnimatePrintScanResultPaper(ScanType.HeartBeatScan));
    }
    
    public void OnClickBehaviourScanButton()
    {
        if (_scanTypeByHasScan[ScanType.BehaviourScan] is false || _playableDirector.state == PlayState.Playing)
        {
            return;
        }
        
        _scanTypeByHasScan[ScanType.BehaviourScan] = false;

        StartCoroutine(AnimatePrintScanResultPaper(ScanType.BehaviourScan));
    }

    public void OnClickWearingScanButton()
    {
        if (_scanTypeByHasScan[ScanType.WearingScan] is false || _playableDirector.state == PlayState.Playing)
        {
            return;
        }

        _scanTypeByHasScan[ScanType.WearingScan] = false;

        StartCoroutine(AnimatePrintScanResultPaper(ScanType.WearingScan));
    }

    public IEnumerator AnimatePrintScanResultPaper(ScanType scanType)
    {
        _playableDirector.playableAsset = _scanResultPrintTimelines[(int)scanType];
        _playableDirector.Play();

        yield return new WaitUntil(() => _playableDirector.state == PlayState.Paused);

        _scanResultPaperGOs[(int)scanType].GetComponent<Image>().raycastTarget = true;
    }
}
