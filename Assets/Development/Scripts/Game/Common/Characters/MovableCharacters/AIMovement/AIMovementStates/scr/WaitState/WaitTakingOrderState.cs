using System.Collections;
using UnityEngine;

public class WaitTakingOrderState : WaitState
{
    protected readonly Customer _customer;

    private CashDesk _table => _objectReference.CashDesk;

    protected override Transform WaitPoint => _table.Wait(_customer);

    public WaitTakingOrderState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
        if (_movement as Customer)
            _customer = _movement as Customer;
    }

    public override Transition GetTransition(AIMovementStateType targetState) => new WantToBeServedTransition(targetState, this);


    protected override IEnumerator Leave()
    {
        _table.EndTransit();
        yield return new WaitForSeconds(0.1f);

        LeaveState();
        _table.Leave();
        StopLeave();
    }

    protected override void Subscribe() => _table.Complite += OnCompliteOrder;
    protected override void Unsubscribe() => _table.Complite -= OnCompliteOrder;


}
