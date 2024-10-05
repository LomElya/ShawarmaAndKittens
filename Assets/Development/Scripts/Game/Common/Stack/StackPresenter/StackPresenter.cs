using System.Collections.Generic;
using UnityEngine;
using Modification;
using System.Linq;

public class StackPresenter : MonoBehaviour, IModificationListener<int>
{
    public event System.Action<StackableType> Added;
    public event System.Action<StackableType> Removed;
    public event System.Action BecameEmpty;
    public event System.Action SetInputType;

    [SerializeField] private StackViewBase _stackView;
    [SerializeField] private StackUIView _stackUIView;
    [SerializeField] private int _stackCapacity;
    [SerializeField] private List<StackableTypes> _allTypesThatCanBeAdded;

    private StackStorage _stack;

    public IEnumerable<StackableType> Data => _stack.Data;

    public bool IsEmpty => _stack.Count == 0;
    public bool IsFull => _stack.Count == _stack.Capacity;
    public int Count => _stack.Count;
    public int Capacity => _stack.Capacity;

    private void Awake() => _stack = new StackStorage(_stackCapacity, _allTypesThatCanBeAdded);
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();
    private void Start() => _stackUIView?.Init(_stack, _stackView);

    public bool CanAddToStack(StackableType stackableType) => _stack.CanAdd(stackableType);
    public bool CanRemoveFromStack(StackableType stackableType) => _stack.Contains(stackableType);

    public void ChangeCapacity(int value) => _stack.ChangeCapacity(value);
    public void OnModificationUpdate(int value) => ChangeCapacity(value);
    public int CalculateCount(StackableType stackableType) => _stack.CalculateCount(stackableType);

    public void AddToStack(StackableType stackable)
    {
        if (CanAddToStack(stackable) == false)
            throw new System.InvalidOperationException();

        _stack.Add(stackable);
        _stackView.Add(stackable);
    }

    public IEnumerable<StackableType> RemoveAll()
    {
        StackableType[] data = _stack.Data.ToArray();
        foreach (StackableType stackable in data)
            RemoveFromStack(stackable);

        return data;
    }

    // public void RemoveAndDestroyAll()
    // {
    //     IEnumerable<StackableType> stackables = RemoveAll();

    //     foreach (StackableType stackable in stackables)
    //         Destroy(stackable.gameObject);
    // }

    // public void RemoveFromStack(Stackable stackable)
    // {
    //     _stack.Remove(stackable);
    //     _stackView.Remove(stackable);
    //     Removed?.Invoke(stackable);

    //     if (_stack.Count == 0)
    //         BecameEmpty?.Invoke();
    // }

    public StackableType RemoveFromStack(StackableType stackableType)
    {
        if (!CanRemoveFromStack(stackableType))
            throw new System.InvalidOperationException();

        StackableType lastStackable = _stack.FindLast(stackableType);

        _stack.Remove(lastStackable);
        _stackView.Remove(lastStackable);
        Removed?.Invoke(lastStackable);

        if (IsEmpty)
            BecameEmpty?.Invoke();

        return lastStackable;
    }

    public int CalculateCount(StackableType[] stackableType)
    {
        int count = 0;
        foreach (StackableType type in stackableType)
            count += _stack.CalculateCount(type);

        return count;
    }

    private void OnStackableMoveEnded(StackableType stackable)
    {
        Added?.Invoke(stackable);
        SetInputType?.Invoke();
    }

    private void RemoveStack(StackableType stackableType) => RemoveFromStack(stackableType);

    private void Subscribe()
    {
        _stackView.MoveEnded += OnStackableMoveEnded;

        if (_stackUIView != null)
            _stackUIView.Remove += RemoveStack;
    }

    private void Unsubscribe()
    {
        _stackView.MoveEnded -= OnStackableMoveEnded;

        if (_stackUIView != null)
            _stackUIView.Remove -= RemoveStack;
    }
}