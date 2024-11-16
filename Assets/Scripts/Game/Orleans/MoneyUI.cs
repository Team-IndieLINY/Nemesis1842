using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _moneyText;

    [SerializeField]
    private TextMeshProUGUI _plusMoneyText;

    [SerializeField]
    private TextMeshProUGUI _minusMoneyText;

    [SerializeField]
    private PlayerOutside _player;
    
    private Vector2 _plusMoneyOriginPosition;
    private Vector2 _minusMoneyOriginPosition;

    public void UpdateMoneyUI()
    {
        _moneyText.text = _player.Money + "$";
        _plusMoneyOriginPosition = _plusMoneyText.rectTransform.anchoredPosition;
        _minusMoneyOriginPosition = _minusMoneyText.rectTransform.anchoredPosition;
    }
    
    public IEnumerator AnimateEarningMoneyText(int startMoney, int endMoney)
    {
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
        
        while (startMoney != endMoney)
        {
            if (endMoney - startMoney >= 0)
                startMoney++;
            else
                startMoney--;

            _moneyText.text = startMoney + "$";

            yield return new WaitForSeconds(0.03f);
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
