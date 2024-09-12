using System.Collections.Generic;
using UnityEngine;
using Modification;
using System;
using System.Linq;

public class StackPresenter : MonoBehaviour, IModificationListener<int>
{
    public event Action<Stackable> Added;
    public event Action<Stackable> Removed;
    public event Action BecameEmpty;
    public event Action SetInputType;

    [SerializeField] private StackView _stackView;
    [SerializeField] private StackUIView _stackUIView;
    [SerializeField] private int _stackCapacity;
    [SerializeField] private List<StackableTypes> _allTypesThatCanBeAdded;

    private StackStorage _stack;

    public IEnumerable<Stackable> Data => _stack.Data;
    public bool IsFull => _stack.Count == _stack.Capacity;
    public int Count => _stack.Count;
    public int Capacity => _stack.Capacity;

    private void Awake() => _stack = new StackStorage(_stackCapacity, _allTypesThatCanBeAdded);
    private void OnEnable() => _stackView.MoveEnded += OnStackableMoveEnded;
    private void OnDisable() => _stackView.MoveEnded -= OnStackableMoveEnded;
    private void Start() => _stackUIView?.Init(_stack, _stackView);

    public bool CanAddToStack(StackableType stackableType) => _stack.CanAdd(stackableType);
    public bool CanRemoveFromStack(StackableType stackableType) => _stack.Contains(stackableType);

    public void ChangeCapacity(int value) => _stack.ChangeCapacity(value);
    public void OnModificationUpdate(int value) => ChangeCapacity(value);
    public int CalculateCount(StackableType stackableType) => _stack.CalculateCount(stackableType);

    public void AddToStack(Stackable stackable)
    {
        if (CanAddToStack(stackable.Type) == false)
            throw new InvalidOperationException();

        _stack.Add(stackable);
        _stackView.Add(stackable);
    }

    public IEnumerable<Stackable> RemoveAll()
    {
        Stackable[] data = _stack.Data.ToArray();
        foreach (Stackable stackable in data)
            RemoveFromStack(stackable);

        return data;
    }

    public void RemoveAndDestroyAll()
    {
        IEnumerable<Stackable> stackables = RemoveAll();

        foreach (Stackable stackable in stackables)
            Destroy(stackable.gameObject);
    }

    public void RemoveFromStack(Stackable stackable)
    {
        _stack.Remove(stackable);
        _stackView.Remove(stackable);
        Removed?.Invoke(stackable);

        if (_stack.Count == 0)
            BecameEmpty?.Invoke();
    }

    public Stackable RemoveFromStack(StackableType stackableType)
    {
        if (CanRemoveFromStack(stackableType) == false)
            throw new InvalidOperationException();

        Stackable lastStackable = _stack.FindLast(stackableType);

        _stack.Remove(lastStackable);
        _stackView.Remove(lastStackable);
        Removed?.Invoke(lastStackable);

        if (_stack.Count == 0)
            BecameEmpty?.Invoke();

        return lastStackable;
    }

    public int CalculateCount(StackableType[] stackableType)
    {
        var count = 0;
        foreach (var type in stackableType)
            count += _stack.CalculateCount(type);

        return count;
    }

    private void OnStackableMoveEnded(Stackable stackable)
    {
        Added?.Invoke(stackable);
        SetInputType?.Invoke();
    }
}
