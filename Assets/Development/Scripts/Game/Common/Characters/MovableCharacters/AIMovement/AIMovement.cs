using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MovementCharacter
{
    private NavMeshAgent _agent;
    private Action _completeAction;

    public bool Completed { get; private set; }

    protected override void OnAwake()
    {
        _agent = GetComponent<NavMeshAgent>();
        enabled = false;
        Completed = false;

        _agent.speed = _speed * _speedRate;
    }

    public override void OnUpdate()
    {
        _view.SetMove(IsMoving);

        if (_agent.pathPending ||
           _agent.pathStatus == NavMeshPathStatus.PathInvalid ||
           _agent.path.corners.Length == 0)
            return;

        if (_agent.remainingDistance < _agent.stoppingDistance + float.Epsilon)
        {
            _completeAction?.Invoke();
            _completeAction = null;

            Completed = true;
            enabled = false;
        }
    }

    public new AIMovement Move(Vector2 target)
    {
        base.Move(target);
        return this;
    }

    protected override void OnMove(Vector2 direction)
    {
        Completed = false;
        _completeAction = null;

        _agent.ResetPath();
        _agent.SetDestination(direction);
        enabled = true;
    }

    protected override void OnStopMove()
    {
        _completeAction = null;
        _agent.ResetPath();
    }

    public void OnComplete(Action completeAction) => _completeAction = completeAction;
    public void Enable() => _agent.enabled = true;
    public void Disable() => _agent.enabled = false;
    public void Look(Vector3 direction) => transform.DOLookAt(transform.position + direction, 1f);
}
