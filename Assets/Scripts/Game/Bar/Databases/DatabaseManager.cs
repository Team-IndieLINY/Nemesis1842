using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField]
    private Database _currentDatabase;

    [SerializeField]
    private Database _mainDatabase;

    public void OpenDatabase(Database nextDatabase)
    {
        if (_currentDatabase == nextDatabase)
        {
            return;
        }

        nextDatabase.gameObject.SetActive(true);
        _currentDatabase.gameObject.SetActive(false);

        _currentDatabase = nextDatabase;
    }

    public virtual void BackButton()
    {
        if(_currentDatabase is CharacterDatabase)
        {
            HomeButton();
            return;
        }

        Database nextDatabase = _currentDatabase.GetPreDatabase();
        if (nextDatabase != null)
        {
            OpenDatabase(nextDatabase);
        }
    }
    
    public virtual void HomeButton()
    {
        if (_currentDatabase is MainDatabase)
        {
            return;
        }

        OpenDatabase(_mainDatabase);
    }

    public Database GetCurrentDatabase() { return _currentDatabase; }
}
