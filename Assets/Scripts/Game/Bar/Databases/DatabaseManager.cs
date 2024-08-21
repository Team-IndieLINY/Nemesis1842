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

    public void BackButton()
    {
        Database nextDatabase = _currentDatabase.GetPreDatabase();
        if (nextDatabase != null)
        {
            OpenDatabase(nextDatabase);
        }
        else
        {
            Debug.Log("최상위 페이지에 도달하였다");
        }
    }
    
    public void HomeButton()
    {
        OpenDatabase(_mainDatabase);
    }

    public Database GetCurrentDatabase() { return _currentDatabase; }
}
