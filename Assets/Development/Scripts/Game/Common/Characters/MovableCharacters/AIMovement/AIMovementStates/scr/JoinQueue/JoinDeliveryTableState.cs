using System.Collections.Generic;

public class JoinDeliveryTableState : JoinQueueState
{

    public JoinDeliveryTableState(AIMovementStateType stateType, AIMovement movement) : base(stateType, movement)
    {
    }

    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.DeliveryQueues.Queues;
    public override Transition GetTransition(AIMovementStateType targetState) => new WaitFreeDeliveryTableTransition(targetState, _movement);
}
