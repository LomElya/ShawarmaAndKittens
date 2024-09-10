using System;
using UnityEngine;

public class AIMovementQueue : Queue<AIMovement>
{
    public event Action<AIMovement> FirstChanged;

    [SerializeField] private int _capacity;
    [SerializeField] private float _offset;

    public override bool NotFull => _queues.Count < _capacity;
    public override bool Empty => _queues.Count == 0;

    public int Capacity => _capacity;

    public void Enqueue(AIMovement movement)
    {
        Add(movement);
    }

    public AIMovement Dequeue()
    {
        if (Empty)
            throw new InvalidOperationException();

        AIMovement movement = _queues[0];
        _queues.RemoveAt(0);

        if (_queues.Count > 0)
            FirstChanged?.Invoke(_queues[0]);

        return movement;
    }

    public AIMovement Peek()
    {
        if (_queues.Count == 0)
            throw new InvalidOperationException();

        return _queues[0];
    }

    protected override void OnAdd(AIMovement movement)
    {
        if (_queues.Count == 1)
            FirstChanged?.Invoke(movement);
    }

    public Vector2 GetPosition(AIMovement parent) => GetPositionOfIndex(_queues.LastIndexOf(parent));
    private Vector2 GetPositionOfIndex(int i) => transform.TransformPoint(new Vector2(0, i * _offset));

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < _capacity; i++)
        {
            var position = GetPositionOfIndex(i);
            Gizmos.DrawSphere(position, 0.2f);
        }
    }

#endif
}
