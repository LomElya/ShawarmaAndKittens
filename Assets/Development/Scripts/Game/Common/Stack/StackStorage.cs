using System.Linq;
using System.Collections.Generic;

public class StackStorage
{
    private readonly List<StackableType> _stackables = new();
    private readonly List<StackableTypes> _allTypesThatCanBeAdded;

    public event System.Action<StackableType> Added;
    public event System.Action<StackableType> Removed;
    public event System.Action CapacityChanged;

    private StackableTypes _currentTypesThatCanBeAdded;
    private int _capacity;

    public int Count => _stackables.Count;
    public int Capacity => _capacity;
    
    public IEnumerable<StackableType> Data => _stackables;

    public StackStorage(int capacity, List<StackableTypes> allTypesThatCanBeAdded)
    {
        _capacity = capacity;
        _allTypesThatCanBeAdded = allTypesThatCanBeAdded;
    }

    public bool CanAdd(StackableType stackableType)
    {
        if (_stackables.Count == 0)
            return FindTypesThatCanBeAdded(stackableType) != null;

        if (_stackables.Count == _capacity)
            return false;

        return _currentTypesThatCanBeAdded.Contains(stackableType);
    }

    public void Add(StackableType stackable)
    {
        if (CanAdd(stackable) == false)
            throw new System.InvalidOperationException(nameof(stackable) + " не может быть добавлен");

        if (_currentTypesThatCanBeAdded == null)
            _currentTypesThatCanBeAdded = FindTypesThatCanBeAdded(stackable);

        _stackables.Add(stackable);
        Added?.Invoke(stackable);
    }

    public void Remove(StackableType stackable)
    {
        if (_stackables.Contains(stackable) == false)
            throw new System.InvalidOperationException(nameof(stackable) + " не добавлен");

        _stackables.Remove(stackable);

        if (_stackables.Count == 0)
            _currentTypesThatCanBeAdded = null;

        Removed?.Invoke(stackable);
    }

    public bool Contains(StackableType stackableType)
    {
        if (_stackables.Count == 0)
            return false;

        foreach (StackableType stackable in _stackables)
            if (stackable == stackableType)
                return true;

        return false;
    }

    public StackableType FindLast(StackableType stackableType)
    {
        if (!Contains(stackableType))
            throw new System.InvalidOperationException(nameof(stackableType) + " нет в наличии");

        return _stackables.FindLast(stackable => stackable == stackableType);
    }

    public void ChangeCapacity(int capacity)
    {
        if (capacity < 0)
            throw new System.ArgumentOutOfRangeException(nameof(capacity));

        _capacity = capacity;
        CapacityChanged?.Invoke();
    }

    public void Clear() => _stackables.Clear();
    public int CalculateCount(StackableType stackableType) => _stackables.Count(stackable => stackable == stackableType);

    private StackableTypes FindTypesThatCanBeAdded(StackableType stackableType) => _allTypesThatCanBeAdded.Find(types => types.Contains(stackableType));
}