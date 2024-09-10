using System.Collections;
using UnityEngine;

public class WaitReceiptOrderState : WaitState
{
    protected readonly Customer _customer;

    private DeliveryTable _table => _objectReference.DeliveryTable;
    private StackPresenter _stackPresenter => _customer.Stack;
    protected override Transform WaitPoint => _table.Wait(_customer);

    public WaitReceiptOrderState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
        if (_movement as Customer)
            _customer = _movement as Customer;
    }

    public override Transition SetTransition(AIMovementStateType targetState) => new WantToBeServedTransition(targetState, this);

    protected override IEnumerator Leave()
    {
        _table.EndTransit();
        yield return new WaitForSeconds(1.5f);

        foreach (Stackable stackable in _table.TakeAllItems())
        {
            _stackPresenter.AddToStack(stackable);
            yield return new WaitForSeconds(0.25f);
        }

        LeaveState();
        _table.Leave();
        StopLeave();
    }

    protected override void Subscribe() => _customer.PurchaseList.BecameEmpty += OnCompliteOrder;
    protected override void Unsubscribe() => _customer.PurchaseList.BecameEmpty -= OnCompliteOrder;


}
