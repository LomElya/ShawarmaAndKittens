using UnityEngine;
using Zenject;

public class LeaveState : AIMovementState
{
    [Inject]
    private CustomerSpawner _customerSpawner;

    private Customer _customer;

    public LeaveState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
    }

    public override Transition GetTransition(AIMovementStateType targetState) => new NoneTransition();

    protected override void onEnter()
    {
        Debug.Log("Уходит");
        _movement.Enable();
        _movement.Move(_customerSpawner.ExitPoint.position).OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        if (_movement as Customer)
        {
            _customer = _movement as Customer;
            _customer.Leave();
            _customerSpawner.DestroyCustomer(_customer);
        }
    }

    protected override void onExit() { }
}
