public class NoneState : AIMovementState
{
    public NoneState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
    }

    public override Transition SetTransition(AIMovementStateType targetState) => new NoneTransition();

    protected override void onEnter() { }
    protected override void onExit() { }

}
