using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    private void Awake()
    {
        _moneyText.text = _money + "$";
    }

    public void EarnMoney(int money)
    {
        int tempMoney = _money;
        _money += money;

        StartCoroutine(AnimateEarningMoneyText(tempMoney, _money));
    }

    private IEnumerator AnimateEarningMoneyText(int startMoney, int endMoney)
    {
        while (startMoney != endMoney)
        {
            startMoney++;
            _moneyText.text = startMoney + "$";

            yield return new WaitForSeconds(0.02f);
        }
    }
}
