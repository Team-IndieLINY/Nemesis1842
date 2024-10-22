using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BelongingData", fileName = "ScriptableObject/BelongingData",order = Int32.MaxValue)]
public class BelongingData : ScriptableObject
{
    [SerializeField]
    private string _guestCode;

    public string GuestCode => _guestCode;
    
    [SerializeField]
    private GameObject _belongingPrefab;

    public GameObject BelongingPrefab => _belongingPrefab;
}
