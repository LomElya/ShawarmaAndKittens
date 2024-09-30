using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientContainer", menuName = "Ingredient/IngredientContainer", order = 1)]
public class IngredientContainer : ScriptableObject
{
    [SerializeField] private List<IngredientData> _datas = new();

    public bool IsOpen(IngredientType type)
    {
        IngredientData data = GetData(type);
        return data.IsOpen;
    }

    public int GetPurchasePrice(IngredientType type)
    {
        IngredientData data = GetData(type);
        return data.PurchasePrice;
    }

    public int GetOpenPrice(IngredientType type)
    {
        IngredientData data = GetData(type);
        return data.OpenPrice;
    }

    public Sprite GetImage(IngredientType type)
    {
        IngredientData data = GetData(type);
        return data.Image;
    }

    public IngredientData GetData(IngredientType type)
    {
        IngredientData data = _datas.FirstOrDefault(x => x.Type == type);

        return data;
    }

    private void OnValidate()
    {
        for (int i = 0; i < _datas.Count; i++)
            _datas[i].Name = _datas[i].Type.ToString();
    }
}

[System.Serializable]
public class IngredientData
{
    [HideInInspector]
    public string Name;

    [SerializeField] private IngredientType _type;
    [SerializeField] private Sprite _image;
    [SerializeField, Range(0, 999999)] private int _openPrice;
    [SerializeField, Range(0, 999999)] private int _purchasePrice;

    public IngredientType Type => _type;
    public Sprite Image => _image;
    public int OpenPrice => _openPrice;
    public int PurchasePrice => _purchasePrice;
    public bool IsOpen => OpenPrice == 0;
}
