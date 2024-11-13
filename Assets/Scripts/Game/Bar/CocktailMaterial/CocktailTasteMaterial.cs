using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CocktailTasteMaterial : CocktailMaterial,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Cocktail.ETasteType _tasteType;
    
    public override void SetCocktail()
    {
        StartCoroutine(_cocktailMakingManager.SetTaste(_tasteType));
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("bottle_hovering");
        _nameTagUI.UpdateNameTagUI(CocktailRecipeBlockUI._tasteTypeByTasteTypeName[_tasteType]);
        _nameTagUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _nameTagUI.gameObject.SetActive(false);
    }
}
