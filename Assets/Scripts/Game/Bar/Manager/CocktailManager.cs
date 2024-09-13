using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CocktailManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _cocktailSpriteRenderer;

    [SerializeField]
    private float _appearCocktailTime = 1f;

    [SerializeField]
    private Sprite[] _cocktailSprites;

    private bool _isAppear = false;
    public bool IsAppear => _isAppear;

    private void Awake()
    {
        _cocktailSpriteRenderer.material.SetColor(
            "_BaseColor", new Color(1f, 1f, 1f, 0f));
    }

    public void SetCocktailSprite(TasteMachine.TasteType? tasteType)
    {
        if (tasteType == null)
        {
            _cocktailSpriteRenderer.sprite = _cocktailSprites[0];
            return;
        }
        
        _cocktailSpriteRenderer.sprite = _cocktailSprites[(int)tasteType];
    }

    public void AppearCocktail()
    {
        StartCoroutine(AppearCocktailCoroutine());
    }

    private IEnumerator AppearCocktailCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _appearCocktailTime)
        {
            _cocktailSpriteRenderer.material.SetColor(
                "_BaseColor", new Color(1f, 1f, 1f, elapsedTime / _appearCocktailTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        _isAppear = true;
    }
    
    public void DisappearCocktail()
    {
        StartCoroutine(DisappearCocktailCoroutine());
    }

    private IEnumerator DisappearCocktailCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _appearCocktailTime)
        {
            _cocktailSpriteRenderer.material.SetColor(
                "_BaseColor", new Color(1f, 1f, 1f, 1f - elapsedTime / _appearCocktailTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        _cocktailSpriteRenderer.material.SetColor(
            "_BaseColor", new Color(1f, 1f, 1f, 0f));
        
        _isAppear = false;
    }
}
