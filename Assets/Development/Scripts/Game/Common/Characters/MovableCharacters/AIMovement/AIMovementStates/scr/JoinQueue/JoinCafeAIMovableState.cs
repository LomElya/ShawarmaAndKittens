using System.Collections.Generic;

public class JoinCafeAIMovableState : JoinQueueState
{
    public JoinCafeAIMovableState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
    }

    protected override IEnumerable<AIMovementQueue> Queues => _queuesReferences.CashDeskQueues.Queues;
    public override Transition SetTransition(AIMovementStateType targetState)=> new WaitFreeCashDeskTransition(targetState, _movement);
}
