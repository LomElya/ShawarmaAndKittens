using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeShawarmaData", menuName = "Stackable/RecipeShawarmaData", order = 1)]
public class RecipeShawarmaData : ScriptableObject
{
    [SerializeField] private StackableType _type;
    [SerializeField, Range(0, 999999)] private int _openPrice;
    // [SerializeField] private int _purchasePrice;
    [SerializeField] private List<IngredientType> _ingredientTypes = new();

    public StackableType Type => _type;
    public IEnumerable<IngredientType> IngredientTypes => _ingredientTypes;

    public int OpenPrice => _openPrice;
    //public int PurchasePrice => _purchasePrice;
    public bool IsOpen => _openPrice == 0;
}
