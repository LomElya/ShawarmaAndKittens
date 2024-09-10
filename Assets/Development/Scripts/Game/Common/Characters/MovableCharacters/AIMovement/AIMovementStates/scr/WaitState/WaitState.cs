using System;
using System.Collections;
using GameHandler;
using UnityEngine;
using Zenject;

public abstract class WaitState : AIMovementState
{
    protected abstract Transform WaitPoint { get; }
    protected InteractableObjectReference _objectReference;

    private UpdateHandler _updateHandler;

    public event Action OnLeaveState;

    [Inject]
    private void Construct(UpdateHandler updateHandler, InteractableObjectReference objectReference)
    {
        _updateHandler = updateHandler;
        _objectReference = objectReference;
    }

    protected WaitState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
    }

    protected override void onEnter()
    {
        Debug.Log("Ждет, когда обслужат");

        _movement.Enable();
        Move(WaitPoint);
        Subscribe();
    }

    protected override void onExit() => Unsubscribe();

    protected void Move(Transform waitPoint)
    {
        _movement.Move(waitPoint.position).OnComplete(() =>
        {
            _movement.Look(-waitPoint.forward);
            _movement.Stop();
            OnCompleateMove();
        });
    }

    protected void OnCompliteOrder()
    {
        Unsubscribe();
        StartLeave();
    }

    protected void StartLeave() => _updateHandler.startCoroutine(Leave());
    protected void StopLeave() => _updateHandler.stopCoroutine(Leave());
    protected void LeaveState() => OnLeaveState?.Invoke();
    protected virtual void OnCompleateMove() { }

    protected abstract IEnumerator Leave();

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}
