using System;
using UnityEngine;
using Zenject;

public abstract class AIMovementState
{
    public event Action<AIMovementStateType> ChangeState;

    public Transition Transition;

    protected readonly AIMovement _movement;
    private readonly AIMovementStateType _stateType;

    public AIMovementStateType StateType => _stateType;

    public AIMovementState(AIMovementStateType stateType, AIMovement movenemt)
    {
        _movement = movenemt;

        _stateType = stateType;
    }

    public void Enter()
    {
        onEnter();
        Transition?.Enable(EndTransit);
    }

    public void Exit()
    {
        Transition?.Disable();
        onExit();
    }

    private void EndTransit() => ChangeState?.Invoke(Transition.TargetStateType);

    protected abstract void onEnter();
    protected abstract void onExit();
    public abstract Transition GetTransition(AIMovementStateType targetState);
    public void SetTransition(Transition transition) => Transition = transition;
}
