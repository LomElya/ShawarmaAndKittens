using UnityEngine;

public abstract class PurchaseListFactory : ScriptableObject
{
    public abstract PurchaseList Create();
    public abstract PurchaseList Create(int count);
}