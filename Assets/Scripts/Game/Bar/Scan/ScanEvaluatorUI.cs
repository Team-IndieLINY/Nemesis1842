using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScanEvaluatorUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scanEvaluationText;
    
    public void UpdateScanEvaluatorUI(bool isSuccess)
    {
        if (isSuccess is true)
        {
            _scanEvaluationText.text = "SUCCESS";
        }
        else
        {
            _scanEvaluationText.text = "FAIL";
        }
    }

    public void ResetScanEvaluatorUI()
    {
        _scanEvaluationText.text = "";
    }
}
