using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CocktailData", order = Int32.MaxValue)]
public class CocktailData : ScriptableObject
{
    public enum TasteType
    {
        Sour,
        Bitter,
        Salty,
        Sweet
    }
    public enum ScentType
    {
        Smoky, // 스모키
        Floral, // 꽃
        Fruity, // 과일
        Nutty, // 견과류
        Malty, //몰트
        Medicinal //약내음
    }

    [SerializeField]
    private TasteType _taste;

    public TasteType Taste => _taste;

    [SerializeField]
    private ScentType _scent;

    public ScentType Scent => _scent;
}