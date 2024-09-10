using System.Collections.Generic;

public class WaitFreeCashDeskTransition : WaitTransition
{
    protected override InteractableObject Target => _objectReference.CashDesk;
    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.CashDeskQueues.Queues;

    public WaitFreeCashDeskTransition(AIMovementStateType traterState, AIMovement parent) : base(traterState, parent)
    {
    }
}
