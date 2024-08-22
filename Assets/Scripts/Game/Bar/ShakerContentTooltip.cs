using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakerContentTooltip : MonoBehaviour
{
    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private TextMeshProUGUI _shakerContentText;

    private readonly Dictionary<TasteMachine.TasteType?, string> _tasteTypeByContentText =
        new()
        {
            { TasteMachine.TasteType.Sweet, "단 맛" },
            { TasteMachine.TasteType.Sour, "신 맛" },
            { TasteMachine.TasteType.Bitter, "쓴 맛" },
            { TasteMachine.TasteType.Salty, "짠 맛" }
        };

    private readonly Dictionary<ScentMachine.ScentType?, string> _scentTypeByContentText =
        new()
        {
            { ScentMachine.ScentType.Smoky, "스모키 향" },
            { ScentMachine.ScentType.Floral, "꽃 향" },
            { ScentMachine.ScentType.Fruity, "과일 향" },
            { ScentMachine.ScentType.Nutty, "견과류 향" },
            { ScentMachine.ScentType.Malty, "몰트 향" },
            { ScentMachine.ScentType.Medicinal, "약내음 향" }
        };

    public void ShowTooltip()
    {
        if (_cocktailMakingManager.Cocktail.TasteType != null)
        {
            _shakerContentText.text += _tasteTypeByContentText[_cocktailMakingManager.Cocktail.TasteType!];
            _shakerContentText.text += '\n';
        }

        if (_cocktailMakingManager.Cocktail.ScentType != null)
        {
            _shakerContentText.text += _scentTypeByContentText[_cocktailMakingManager.Cocktail.ScentType!];
            _shakerContentText.text += '\n';
        }
        
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        _shakerContentText.text = "";
        gameObject.SetActive(false);
    }
}
