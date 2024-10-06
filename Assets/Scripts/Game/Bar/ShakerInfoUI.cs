using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakerInfoUI : MonoBehaviour
{
    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private TextMeshProUGUI _currentTasteTypeText;

    [SerializeField]
    private TextMeshProUGUI _currentScentTypeText;
    
    private Dictionary<Cocktail.ETasteType, string> _tasteTypeByTasteTypeName
        = new()
        {
            { Cocktail.ETasteType.SWEET, "단 맛" },
            { Cocktail.ETasteType.SOUR, "신 맛" },
            { Cocktail.ETasteType.BITTER, "쓴 맛" },
            { Cocktail.ETasteType.SALTY, "짠 맛" }
        };

    private Dictionary<Cocktail.EScentType, string> _scentTypeByScentTypeName
        = new()
        {
            { Cocktail.EScentType.FRUITY, "과일향" },
            { Cocktail.EScentType.FLORAL, "꽃향" },
            { Cocktail.EScentType.SMOKY, "스모키향" },
            { Cocktail.EScentType.MEDICINAL, "약향" },
            { Cocktail.EScentType.MALTY, "몰티향" },
            { Cocktail.EScentType.NUTTY, "견과류향" }
        };


    public void UpdateShakerInfoUI()
    {
        if (_cocktailMakingManager.Cocktail.TasteType != null)
        {
            _currentTasteTypeText.text = "맛: " +
                _tasteTypeByTasteTypeName[(Cocktail.ETasteType)_cocktailMakingManager.Cocktail.TasteType];
        }
        else
        {
            _currentTasteTypeText.text = "맛: ";
        }

        if (_cocktailMakingManager.Cocktail.ScentType != null)
        {
            _currentScentTypeText.text = "향: " +
                _scentTypeByScentTypeName[(Cocktail.EScentType)_cocktailMakingManager.Cocktail.ScentType];
        }
        else
        {
            _currentScentTypeText.text = "향: ";
        }
    }

    public void ResetShakerInfoUI()
    {
        _currentTasteTypeText.text = "맛: ";
        _currentScentTypeText.text = "향: ";
    }
}
