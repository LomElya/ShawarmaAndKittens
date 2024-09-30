using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackInventoryList : StackUIView
{
    [SerializeField] private List<StackInventoryView> _views = new();
    [SerializeField] private IconByStackable _iconByStackable;

    protected override void OnInit(StackStorage stack, IStackableContainer stackableContainer) =>
        _views.ForEach(view => view.Init(_iconByStackable));

    protected override void OnAdded(StackableType stackable)
    {
        if (Contains(stackable))
            return;

        StackInventoryView view = GetEmptyView();
        view.Add(stackable);
    }

    protected override void OnRemoved(StackableType stackable)
    {
        if (!Contains(stackable))
            return;

        StackInventoryView view = GetView(stackable);
        view.Remove();
    }

    protected override void OnCapacityChanged() { }

    private bool Contains(StackableType type) => _views.Any(view => view.Type == type && view.Empty == false);
    private StackInventoryView GetEmptyView() => _views.FirstOrDefault(view => view.Empty == true);
    private StackInventoryView GetView(StackableType type) => _views.FirstOrDefault(view => view.Type == type);

    private void OnSelectedSlot(StackInventoryView selectedView)
    {
        _views.ForEach(view =>
        {
            if (view != selectedView)
                view.Unselect();
        });
    }

    protected override void OnSubscribe()
    {
        _views.ForEach(view =>
              {
                  view.RemoveStot += InvokeRemove;
                  view.OnSelected += OnSelectedSlot;
              });
    }

    protected override void OnUnsubscribe()
    {
        _views.ForEach(view =>
               {
                   view.RemoveStot -= InvokeRemove;
                   view.OnSelected -= OnSelectedSlot;
               });
    }
}
