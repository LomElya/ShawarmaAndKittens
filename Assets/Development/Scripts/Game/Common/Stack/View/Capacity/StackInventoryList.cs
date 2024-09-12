using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackInventoryList : MonoBehaviour
{
    [SerializeField] private List<StackInventoryView> _views = new();
    [SerializeField] private IconByStackable _iconByStackable;

    private StackPresenter _stack;

    public void Init(StackPresenter stack)
    {
        _stack = stack;
        _views.ForEach(view => view.Init(_iconByStackable));
        Subscribe();
    }

    private void OnAddedStack(Stackable stackable)
    {
        StackableType type = stackable.Type;

        if (Contains(type))
            return;

        StackInventoryView view = GetEmptyView();
        view.Add(type);
    }

    private void OnRemovedStack(Stackable stackable)
    {
        StackableType type = stackable.Type;

        if (!Contains(type))
            return;

        StackInventoryView view = GetView(type);
        view.Remove();
    }

    private bool Contains(StackableType type) => _views.Any(view => view.Type == type && view.Empty == false);
    private StackInventoryView GetEmptyView() => _views.FirstOrDefault(view => view.Empty == true);
    private StackInventoryView GetView(StackableType type) => _views.FirstOrDefault(view => view.Type == type);

    private void Subscribe()
    {
        _stack.Added += OnAddedStack;
        _stack.Removed += OnRemovedStack;
    }

    private void Unsubscribe()
    {
        if (_stack == null)
            return;

        _stack.Added -= OnAddedStack;
        _stack.Removed -= OnRemovedStack;
    }

    private void OnDestroy() => Unsubscribe();
}
