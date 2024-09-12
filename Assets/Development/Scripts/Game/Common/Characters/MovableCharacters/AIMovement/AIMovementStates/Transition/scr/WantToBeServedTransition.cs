public class WantToBeServedTransition : Transition
{
    private readonly WaitState _stateParent;

    public WantToBeServedTransition(AIMovementStateType traterState, WaitState stateParent) : base(traterState)
    {
        _stateParent = stateParent;
    }

    protected override void onEnable() => Subscribe();
    protected override void onDisable() => Unsubscribe();

    protected void OnLeaveState()
    {
        Unsubscribe();
        EndTransit();
    }

    private void Subscribe() => _stateParent.OnLeaveState += OnLeaveState;
    private void Unsubscribe() => _stateParent.OnLeaveState -= OnLeaveState;
}
