using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class JoinQueueState : AIMovementState
{
    [Inject]
    protected QueuesReferences _queuesReferences;

    private AIMovementQueue _movementQueue;

    protected abstract IEnumerable<AIMovementQueue> Queues { get; }

    public JoinQueueState(AIMovementStateType stateType, AIMovementStateType targetState, AIMovement movenemt) : base(stateType, targetState, movenemt)
    {
    }

    protected override void onEnter()
    {
        Debug.Log("Встал в очередь");
        _movement.Enable();

        foreach (AIMovementQueue queue in Queues)
        {
            if (queue.NotFull)
            {
                _movementQueue = queue;
                _movementQueue.Enqueue(_movement);
                GoToPositionInQueue();

                if (_movementQueue.Peek() != _movement)
                    _movementQueue.FirstChanged += OnFirstChanged;

                break;
            }
        }

        OnEnter();
    }

    protected override void onExit()
    {
        if (_movementQueue == null)
            return;

        if (_movementQueue.Peek() == _movement)
            _movementQueue.Dequeue();
        else
            _movementQueue.Remove(_movement);

        _movement.Disable();
        //_customer.CreateNewPurchaseList();
        _movementQueue.FirstChanged -= OnFirstChanged;

        OnExit();
    }

    private void OnFirstChanged(AIMovement movement) => GoToPositionInQueue();

    private void GoToPositionInQueue()
    {
        _movement.Move(_movementQueue.GetPosition(_movement));
        _movement.OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        _movement.Stop();
        _movement.Look(-_movementQueue.transform.forward);
    }

    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
}
