using System.Collections.Generic;

public class JoinCafeAIMovableState : JoinQueueState
{
    public JoinCafeAIMovableState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
    }

    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.CashDeskQueues.Queues;
    public override Transition GetTransition(AIMovementStateType targetState) => new WaitFreeCashDeskTransition(targetState, _movement);
}
