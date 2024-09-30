using UnityEngine;

public abstract class StackableProvider : MonoBehaviour
{
    public abstract StackableType InstantiateStackable();
    public abstract StackableType GetStackable();
}