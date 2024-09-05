using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnowledgeDatabase : Database
{
    [SerializeField]
    private TextMeshProUGUI _nameLabel;

    [SerializeField]
    private TextMeshProUGUI _baseInfoLabel;

    [SerializeField]
    private TextMeshProUGUI _coreInfoLabel;
    
    public override void OpenDatabase()
    {
        this._preDatabase = _databaseManager.GetCurrentDatabase();
        
        base.OpenDatabase();
    }

    public void SetDatabase(DatabaseData databaseData)
    {
        KnowledgeDatabaseData knowledgeDatabaseData = databaseData as KnowledgeDatabaseData;

        if (knowledgeDatabaseData == null)
        {
            Debug.LogError("knowledgeDatabaseData is null");
            return;
        }
        
        _nameLabel.text = knowledgeDatabaseData.NameLabel;
        _baseInfoLabel.text = knowledgeDatabaseData.BaseInfoLabel;
        _coreInfoLabel.text = knowledgeDatabaseData.CoreInfoLabel;
    }
}