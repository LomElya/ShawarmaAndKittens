using System;

public abstract class AIMovementState
{
    public event Action<AIMovementStateType> ChangeState;

    public Transition Transition;

    protected readonly AIMovement _movement;
    private readonly AIMovementStateType _stateType;

    public AIMovementStateType StateType => _stateType;

    public AIMovementState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt)
    {
        _movement = movenemt;

        _stateType = stateType;
        Transition = SetTransition(targetState);
    }

    public void Enter()
    {
        Transition?.Enable(EndTransit);
        onEnter();
    }

    public void Exit()
    {
        Transition?.Disable();
        onExit();
    }

    private void EndTransit() => ChangeState?.Invoke(Transition.TargetStateType);

    protected abstract void onEnter();
    protected abstract void onExit();
    public abstract Transition SetTransition(AIMovementStateType targetState);
}
