using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class WaitTransition : Transition
{
    protected QueuesReferences _queuesReferences;
    protected InteractableObjectReference _objectReference;

    [Inject]
    private void Construct(QueuesReferences queuesReferences, InteractableObjectReference objectReference)
    {
        _queuesReferences = queuesReferences;
        _objectReference = objectReference;
    }

    protected readonly AIMovement _parent;

    protected abstract InteractableObject Target { get; }
    protected abstract IEnumerable<AIMovementQueue> Queues { get; }

    public WaitTransition(AIMovementStateType traterState, AIMovement parent) : base(traterState)
    {
        _parent = parent;
    }

    protected override void onEnable()
    {
        if (CanTransit())
        {
            EndTransit();
            return;
        }

        Subscribe();
    }

    protected override void onDisable() => Unsubscribe();

    private void OnBecameEmpty()
    {
        if (!CanEndTransit())
            return;

        Unsubscribe();
        EndTransit();
    }

    private bool CanTransit() => Target.Free;
    private bool CanEndTransit() => !Queues.Any(queue => queue.Peek() != _parent) && CanTransit();

    private void Subscribe() => Target.BecameFree += OnBecameEmpty;
    private void Unsubscribe() => Target.BecameFree -= OnBecameEmpty;
}
