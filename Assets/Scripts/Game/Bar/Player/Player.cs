using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
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
        _money = Mathf.Clamp(_money + money, 0, Int32.MaxValue);

        _moneyText.DOColor(new Color32(49, 255, 52, 255), 0.2f);
        _moneyText.transform.DOScale(new Vector3(1.8f, 1.8f, 1f), 0.2f);
        
        StartCoroutine(AnimateEarningMoneyText(tempMoney, _money));
    }

    private IEnumerator AnimateEarningMoneyText(int startMoney, int endMoney)
    {
        while (startMoney != endMoney)
        {
            startMoney++;
            _moneyText.text = startMoney + "$";

            yield return new WaitForSeconds(0.03f);
        }

        _moneyText.DOKill();
        _moneyText.transform.DOKill();

        _moneyText.DOColor(new Color32(255, 255, 255, 255), 0.2f);
        _moneyText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }
}
