using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeShawarma", menuName = "Stackable/RecipeShawarma", order = 1)]
public class RecipeShawarma : ScriptableObject
{
    [SerializeField] private List<RecipeShawarmaData> _datas = new();

    public IEnumerable<IngredientType> GetIngredients(StackableType type)
    {
        if (type == StackableType.None)
        {
            Debug.LogError("Передано None зачение {" + type + "}");
            return null;
        }

        RecipeShawarmaData data = _datas.FirstOrDefault(data => data.Type == type);

        if (data == null)
        {
            Debug.LogError("Рецепта {" + type + "} не найдено");
            return null;
        }

        return data.IngredientTypes;
    }
}

