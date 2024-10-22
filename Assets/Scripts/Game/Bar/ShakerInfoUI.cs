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
            { Cocktail.EScentType.SCIENTICCINNAMON, "사이언틱 시나몬" },
            { Cocktail.EScentType.BASILBLEND, "바질 블랜드" },
            { Cocktail.EScentType.STARDUSTSUGAR, "스타더스트 슈가" },
            { Cocktail.EScentType.COLDMOON, "콜드 문" },
            { Cocktail.EScentType.METEORALMOND, "유성 아몬드" },
            { Cocktail.EScentType.PINKBLUEMING, "핑크 블루밍" }
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
