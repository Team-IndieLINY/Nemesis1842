using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TasteTypeButton : MonoBehaviour
{
    [SerializeField]
    private TasteMachine _tasteMachine;
    [SerializeField]
    private TasteMachine.TasteType _tasteType;

    public void OnClickTasteTypeButton()
    {
        _tasteMachine.SetCurrentTasteType(_tasteType);  
    }
}
