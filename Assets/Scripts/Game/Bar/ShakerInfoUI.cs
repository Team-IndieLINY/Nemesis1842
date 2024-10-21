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
            { Cocktail.ETasteType.JEGERMEISTER, "지거 마이스티" },
            { Cocktail.ETasteType.KAHLUA, "꿀루아" },
            { Cocktail.ETasteType.MALIBU, "물리부" },
            { Cocktail.ETasteType.PEACHTREE, "레몬 트리" }
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
        if (_cocktailMakingManager.TasteType != null)
        {
            _currentTasteTypeText.text =
                _tasteTypeByTasteTypeName[(Cocktail.ETasteType)_cocktailMakingManager.TasteType];
        }
        else
        {
            _currentTasteTypeText.text = "";
        }

        if (_cocktailMakingManager.ScentType != null)
        {
            _currentScentTypeText.text =
                _scentTypeByScentTypeName[(Cocktail.EScentType)_cocktailMakingManager.ScentType];
        }
        else
        {
            _currentScentTypeText.text = "";
        }
    }

    public void ResetShakerInfoUI()
    {
        _currentTasteTypeText.text = "";
        _currentScentTypeText.text = "";
    }
}
