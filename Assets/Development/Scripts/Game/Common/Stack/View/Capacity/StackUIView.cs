using UnityEngine;

public abstract class StackUIView : MonoBehaviour
{
    public event System.Action<StackableType> Remove;

    protected StackStorage _stack;
    protected IStackableContainer _stackableContainer;

    private void OnEnable() => Enable();

    private void OnDisable()
    {
        Unsubscribe();

        Disable();
    }

    public void Init(StackStorage stack, IStackableContainer stackableContainer)
    {
        _stack = stack;
        _stackableContainer = stackableContainer;

        Subscribe();
        OnInit(stack, stackableContainer);
    }

    protected abstract void OnAdded(StackableType stackable);
    protected abstract void OnRemoved(StackableType stackable);
    protected abstract void OnCapacityChanged();

    protected virtual void Enable() { }
    protected virtual void Disable() { }
    protected virtual void OnInit(StackStorage stack, IStackableContainer stackableContainer) { }
    protected virtual void OnSubscribe() { }
    protected virtual void OnUnsubscribe() { }

    private void Subscribe()
    {
        if (_stackableContainer != null)
        {
            _stackableContainer.Added += OnAdded;
            _stackableContainer.Removed += OnRemoved;
        }

        if (_stack != null)
            _stack.CapacityChanged += OnCapacityChanged;

        OnSubscribe();
    }

    private void Unsubscribe()
    {
        if (_stackableContainer != null)
        {
            _stackableContainer.Added -= OnAdded;
            _stackableContainer.Removed -= OnRemoved;
        }

        if (_stack != null)
            _stack.CapacityChanged -= OnCapacityChanged;

        OnUnsubscribe();
    }

    protected void InvokeRemove(StackableType stackableType) => Remove?.Invoke(stackableType);
    private void OnDestroy() => Unsubscribe();
}
