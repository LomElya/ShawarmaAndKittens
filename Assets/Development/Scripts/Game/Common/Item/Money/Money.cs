using UnityEngine;

public class Money : Stackable
{
    [SerializeField] private int _value = 1;

    public override StackableType Type => StackableType.Money;
    public int Value => _value;
}
