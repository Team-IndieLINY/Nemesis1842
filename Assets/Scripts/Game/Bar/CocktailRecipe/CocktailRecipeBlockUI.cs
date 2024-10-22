using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CocktailRecipeBlockUI : MonoBehaviour
{
    [SerializeField]
    private Image _cocktailImage;
    
    [SerializeField]
    private TextMeshProUGUI _cocktailNameText;
    
    [SerializeField]
    private TextMeshProUGUI _cocktailDescriptionText;

    [SerializeField]
    private TextMeshProUGUI _cocktailRecipeText;

    public static Dictionary<Cocktail.ETasteType, string> _tasteTypeByTasteTypeName
        = new()
        {
            { Cocktail.ETasteType.JEGERMEISTER, "지거 마이스티" },
            { Cocktail.ETasteType.KAHLUA, "꿀루아" },
            { Cocktail.ETasteType.MALIBU, "물리부" },
            { Cocktail.ETasteType.PEACHTREE, "레몬 트리" }
        };

    public static Dictionary<Cocktail.EScentType, string> _scentTypeByScentTypeName
        = new()
        {
            { Cocktail.EScentType.SCIENTICCINNAMON, "사이언틱 시나몬" },
            { Cocktail.EScentType.BASILBLEND, "바질 블랜드" },
            { Cocktail.EScentType.STARDUSTSUGAR, "스타더스트 슈가" },
            { Cocktail.EScentType.COLDMOON, "콜드 문" },
            { Cocktail.EScentType.METEORALMOND, "유성 아몬드" },
            { Cocktail.EScentType.PINKBLUEMING, "핑크 블루밍" }
        };

    public void InitializeCocktailRecipeBlockUI(CocktailData cocktailData)
    {
        _cocktailImage.sprite = cocktailData.CocktailSprite;
        _cocktailNameText.text = cocktailData.CocktailName;
        _cocktailDescriptionText.text = cocktailData.CocktailDescription;
        _cocktailRecipeText.text = _tasteTypeByTasteTypeName[cocktailData.TasteType] + " + " +
                                   _scentTypeByScentTypeName[cocktailData.ScentType];
    }
}
