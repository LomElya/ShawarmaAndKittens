using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Purchase/Create ShopPurchaseListFactory", fileName = "ShopPurchaseListFactory", order = 0)]
public class ShopPurchaseListFactory : PurchaseListFactory
{
    [SerializeField] private List<UnlockableStackableItem> _items;
    [SerializeField] private int _maxCount = 1;

    public override PurchaseList Create() =>
        Create(Random.Range(1, _maxCount + 1));

    public override PurchaseList Create(int count)
    {
        PurchaseList newList = new PurchaseList()
            .Add(StackableType.Shawarma, count);

        return newList;
    }

    private UnlockableStackableItem FindUnlockedStackable(StackableType stackableType) =>
        _items.FirstOrDefault(Unlocked(stackableType));

    private static Func<UnlockableStackableItem, bool> Unlocked(StackableType stackableType) =>
        item => item.StackableType == stackableType;
}

[Serializable]
public class UnlockableStackableItem
{
    [field: SerializeField] public StackableType StackableType { get; private set; }
}