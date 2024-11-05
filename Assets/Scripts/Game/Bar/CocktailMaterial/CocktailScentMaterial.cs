using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CocktailScentMaterial : CocktailMaterial,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Cocktail.EScentType _scentType;
    
    
    public override void SetCocktail()
    {
        _cocktailMakingManager.SetScent(_scentType);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("bottle_hovering");
        _nameTagUI.UpdateNameTagUI(CocktailRecipeBlockUI._scentTypeByScentTypeName[_scentType]);
        _nameTagUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _nameTagUI.gameObject.SetActive(false);
    }
}
