using UnityEngine;

public class LeaveState : AIMovementStates
{
    public LeaveState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
    }

    public override Transition SetTransition() => new NoneTransition();

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
