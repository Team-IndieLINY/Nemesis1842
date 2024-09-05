using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/KnowledgeDatabaseData", fileName = "KnowledgeDatabaseData", order = Int32.MaxValue)]
public class KnowledgeDatabaseData : DatabaseData
{
    [SerializeField]
    private string _nameLabel;
    public string NameLabel => _nameLabel;
    
    [SerializeField]
    private string _baseInfoLabel;
    public string BaseInfoLabel => _baseInfoLabel;
    
    [SerializeField]
    private string _coreInfoLabel;
    public string CoreInfoLabel => _coreInfoLabel;
}