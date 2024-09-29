using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanCardPool : MonoBehaviour
{
    public static ScanCardPool Inst { get; private set; }
    
    [SerializeField]
    private int _scanCardSpawnCount;
    [SerializeField]
    private GameObject _scanCardPrefab;
    
    [SerializeField]
    private RectTransform _scanCardSpawnTransform;
    
    private GameObject[] _scanCards;

    private void Awake()
    {
        Inst = this;
        _scanCards = new GameObject[_scanCardSpawnCount];
        
        for (int i = 0; i < _scanCardSpawnCount; i++)
        {
            _scanCards[i] = 
                Instantiate(_scanCardPrefab, _scanCardSpawnTransform.anchoredPosition, Quaternion.identity, transform);
            _scanCards[i].SetActive(false);
        }
    }
    
    public GameObject GetScanCardInPool()
    {
        for (int i = 0; i < _scanCardSpawnCount; i++)
        {
            if (_scanCards[i].activeSelf is false)
            {
                _scanCards[i].SetActive(true);
                return _scanCards[i];
            }
        }
        
        Debug.Log("Pool is Empty");

        return null;
    }

    public void ReturnScanCardToPool(GameObject scanCard)
    {
        scanCard.transform.SetParent(transform);
        scanCard.GetComponent<RectTransform>().anchoredPosition = _scanCardSpawnTransform.anchoredPosition;
        scanCard.SetActive(false);
    }
}