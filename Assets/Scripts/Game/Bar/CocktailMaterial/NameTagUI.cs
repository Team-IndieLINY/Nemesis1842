using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTagUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    
    public void UpdateNameTagUI(string materialName)
    {
        _nameText.text = materialName;
    }
}
