using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabaseManager : DatabaseManager
{
    [SerializeField]
    private CharacterDatabase _characterDatabase;

    private void Awake()
    {
        _characterDatabase.Initialize();
    }

    public override void BackButton()
    {
        _characterDatabase.ResetCurrentDataType();
        base.BackButton();
    }

    public override void HomeButton()
    {
        _characterDatabase.ResetCurrentDataType();
        base.HomeButton();
    }
}
