using UnityEngine;

public class MoneyTrigger : Trigger<DroppableMoney>
{
    protected override int Layer => LayerMask.NameToLayer("Default");
}
