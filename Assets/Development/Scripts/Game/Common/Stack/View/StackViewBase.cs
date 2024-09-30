using UnityEngine;

public abstract class StackViewBase : MonoBehaviour, IStackableContainer
{
    public event System.Action<StackableType> Added;
    public event System.Action<StackableType> Removed;
    public event System.Action<StackableType> MoveEnded;

    public void Add(StackableType stackable)
    {
        onAdd(stackable);
        Added?.Invoke(stackable);
    }

    public void Remove(StackableType stackable)
    {
        onRemove(stackable);
        Removed?.Invoke(stackable);
    }

    protected abstract void onAdd(StackableType stackable);
    protected abstract void onRemove(StackableType stackable);

    protected void InvokeMoveEnded(StackableType stackable) => MoveEnded?.Invoke(stackable);

    public virtual float FindTopPositionY() { return 0f; }
}

public interface IStackableContainer
{
    event System.Action<StackableType> Added;
    event System.Action<StackableType> Removed;

    float FindTopPositionY();
}
