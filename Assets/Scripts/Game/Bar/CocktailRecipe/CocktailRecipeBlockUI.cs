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

    public void InitializeCocktailRecipeBlockUI(CocktailData cocktailData)
    {
        _cocktailImage.sprite = cocktailData.CocktailSprite;
        _cocktailNameText.text = cocktailData.CocktailName;
        _cocktailDescriptionText.text = cocktailData.CocktailDescription;
        _cocktailRecipeText.text = _tasteTypeByTasteTypeName[cocktailData.TasteType] + " + " +
                                   _scentTypeByScentTypeName[cocktailData.ScentType];
    }
}
