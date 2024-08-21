using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
public abstract class Database : MonoBehaviour
{
    [SerializeField]
    protected DatabaseManager _databaseManager;

    [SerializeField]
    protected Database _preDatabase;

    public virtual void OpenDatabase()
    {
        _databaseManager.OpenDatabase(this);
    }

    public Database GetPreDatabase()
    {
        return _preDatabase;
    }
}
