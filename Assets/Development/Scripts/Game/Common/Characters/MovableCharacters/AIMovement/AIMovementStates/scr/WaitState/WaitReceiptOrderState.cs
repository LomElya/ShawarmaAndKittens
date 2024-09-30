using System.Collections;
using UnityEngine;

public class WaitReceiptOrderState : WaitState
{
    protected readonly Customer _customer;

    private DeliveryTable _table => _objectReference.DeliveryTable;
    private StackPresenter _stackPresenter => _customer.Stack;
    protected override Transform WaitPoint => _table.Wait(_customer);

    public WaitReceiptOrderState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
        if (_movement as Customer)
            _customer = _movement as Customer;
    }

    public override Transition GetTransition(AIMovementStateType targetState) => new WantToBeServedTransition(targetState, this);

    protected override IEnumerator Leave()
    {
        _table.EndTransit();
        yield return new WaitForSeconds(1f);

        foreach (StackableType stackable in _table.TakeAllItems())
        {
            _stackPresenter.AddToStack(stackable);
            yield return new WaitForSeconds(0.15f);
        }

        LeaveState();
        _table.Leave();
       // _stackPresenter.RemoveAndDestroyAll();
        _stackPresenter.RemoveAll();
        StopLeave();
    }

    protected override void Subscribe() => _customer.PurchaseList.BecameEmpty += OnCompliteOrder;
    protected override void Unsubscribe() => _customer.PurchaseList.BecameEmpty -= OnCompliteOrder;


}
