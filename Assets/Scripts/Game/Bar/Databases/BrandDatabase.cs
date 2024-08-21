using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandDatabase : Database
{
    public override void OpenDatabase()
    {
        this._preDatabase = _databaseManager.GetCurrentDatabase();
        base.OpenDatabase();
    }
}
