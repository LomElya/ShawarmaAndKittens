using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Queue<T> : MonoBehaviour
{
    protected List<T> _queues = new();

    public IEnumerable<T> Queues => _queues;

    public abstract bool NotFull { get; }
    public abstract bool Empty { get; }

    public void Add(T queue)
    {
        if (NotFull == false)
            throw new InvalidOperationException();

        _queues.Add(queue);
        OnAdd(queue);
    }

    public void Remove(T queue)
    {
        if (!_queues.Contains(queue))
            throw new InvalidOperationException();

        OnRemove(queue);
        _queues.Remove(queue);
    }

    protected virtual void OnAdd(T queue) { }
    protected virtual void OnRemove(T queue) { }
}
