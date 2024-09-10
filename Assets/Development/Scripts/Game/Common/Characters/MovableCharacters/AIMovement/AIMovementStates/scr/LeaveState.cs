using UnityEngine;

public class LeaveState : AIMovementState
{
    public LeaveState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
    }

    public override Transition SetTransition(AIMovementStateType targetState)=> new NoneTransition();

    protected override void onEnter()
    {
        Debug.Log("Уходит");
        _movement.Enable();
        // _movement.Move(_parent.References.ExitPoint.position).OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        // _customer.Leave();
        // _customerSpawner.DestroyCustomer(_customer);
    }

    protected override void onExit() { }


}
