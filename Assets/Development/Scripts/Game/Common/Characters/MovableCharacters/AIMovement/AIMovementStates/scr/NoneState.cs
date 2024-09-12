public class NoneState : AIMovementState
{
    public NoneState(AIMovementStateType stateType, AIMovement movenemt) : base(stateType, movenemt)
    {
    }

    public override Transition GetTransition(AIMovementStateType targetState) => new NoneTransition();

    protected override void onEnter() { }
    protected override void onExit() { }

}
