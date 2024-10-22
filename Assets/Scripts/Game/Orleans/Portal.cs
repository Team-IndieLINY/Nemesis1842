using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject _keyToolTipGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerController.Inst.CanEntry)
        {
            MoveScene();
        }
    }

    public void ActivateKeyToolTip()
    {
        _keyToolTipGO.SetActive(true);
    }
    
    public void InActivateKeyToolTip()
    {
        _keyToolTipGO.SetActive(false);
    }
    
    public void MoveScene()
    {
        SceneManager.LoadScene("Bar");
    }
}
