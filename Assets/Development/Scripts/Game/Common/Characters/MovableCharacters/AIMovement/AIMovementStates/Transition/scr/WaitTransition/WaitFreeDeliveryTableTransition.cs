using System.Collections.Generic;

public class WaitFreeDeliveryTableTransition : WaitTransition
{
    protected override InteractableObject Target => _objectReference.DeliveryTable;
    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.DeliveryQueues.Queues;

    public WaitFreeDeliveryTableTransition(AIMovementStateType traterState, AIMovement parent) : base(traterState, parent)
    {
    }
}
