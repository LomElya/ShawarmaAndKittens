public class NoneState : AIMovementStates
{
    public NoneState(AIMovementStateType stateType, AICharacter parent) : base(stateType, parent) { }
    protected override void onEnter() { }
    protected override void onExit() { }

    public override Transition SetTransition() => new NoneTransition();
}
