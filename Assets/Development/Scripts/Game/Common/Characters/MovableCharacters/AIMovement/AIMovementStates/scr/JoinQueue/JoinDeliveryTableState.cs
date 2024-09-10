using System.Collections.Generic;

public class JoinDeliveryTableState : JoinQueueState
{

    public JoinDeliveryTableState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movement) : base(stateType, targetState, movement)
    {
    }

    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.DeliveryQueues.Queues;
    public override Transition SetTransition(AIMovementStateType targetState) => new WaitFreeDeliveryTableTransition(targetState, _movement);
}
