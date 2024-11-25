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

    public int Money => _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    [SerializeField]
    private TextMeshProUGUI _plusMoneyText;

    [SerializeField]
    private TextMeshProUGUI _minusMoneyText;
    
    [SerializeField]
    private float _moneyAnimationTime;

    private Vector2 _plusMoneyOriginPosition;
    private Vector2 _minusMoneyOriginPosition;

    private void Awake()
    {
        LoadMoney();
        _moneyText.text = _money.ToString();
        _plusMoneyOriginPosition = _plusMoneyText.rectTransform.anchoredPosition;
        _minusMoneyOriginPosition = _minusMoneyText.rectTransform.anchoredPosition;
    }

    public void EarnMoney(int money)
    {
        int tempMoney = _money;
        _money = Mathf.Clamp(_money + money, 0, Int32.MaxValue);
        
        StartCoroutine(AnimateEarningMoneyText(tempMoney, _money));
    }

    private void LoadMoney()
    {
        _money = PlayerManager.Instance().Money;
    }

    public void SaveMoney()
    {
        PlayerManager.Instance().SaveMoneyData(_money);
    }

    private IEnumerator AnimateEarningMoneyText(int startMoney, int endMoney)
    {
        AudioManager.Inst.PlaySFX("money_increase");
        TextMeshProUGUI text;
        Vector2 originPosition;
        string moneyString;
        if (endMoney - startMoney >= 0)
        {
            text = _plusMoneyText;
            originPosition = _plusMoneyOriginPosition;
            moneyString = "+" + (endMoney - startMoney);
        }
        else
        {
            text = _minusMoneyText;
            originPosition = _minusMoneyOriginPosition;
            moneyString = (endMoney - startMoney).ToString();
        }

        text.text = moneyString;
        text.DOFade(1f, 0.3f);

        Vector2 upPosition = new Vector2(originPosition.x, originPosition.y + 20f);
        text.rectTransform.DOAnchorPos(upPosition, 1f);
        
        float time = 1 / Mathf.Abs(endMoney - startMoney);
        
        while (startMoney != endMoney)
        {
            if (endMoney - startMoney >= 0)
                startMoney++;
            else
                startMoney--;

            _moneyText.text = startMoney.ToString();

            yield return new WaitForSeconds(time * _moneyAnimationTime);
        }

        text.DOKill();

        text.DOFade(0f, 0.3f)
            .OnKill(() =>
            {
                text.rectTransform.DOKill();
                text.rectTransform.anchoredPosition = originPosition;
            });
    }
}
