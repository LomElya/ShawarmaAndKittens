using System;
using UnityEngine;

public class TimerProducer : TimerInteractableZone
{
    public event Action StackedInteraction;
    public event Action<Stackable> ItemGave;
    public event Action<StackPresenter> Enter;
    public event Action<StackPresenter> Exit;

    [SerializeField] private SingleTypeStackableProvider _stackableProvider;
    [SerializeField] private int _maxCount = 1;

    public StackableType Type => _stackableProvider.Type;
    public int MaxCount => _maxCount;

    public override void Entered(StackPresenter enteredStack)
    {
        base.Entered(enteredStack);
        Enter?.Invoke(enteredStack);
    }

    public override void Exited(StackPresenter otherStack)
    {
        base.Exited(otherStack);
        Exit?.Invoke(otherStack);
    }

    public override bool CanInteract(StackPresenter enteredStack)
        => enteredStack.CanAddToStack(_stackableProvider.Type) &&
            enteredStack.CalculateCount(_stackableProvider.Type) < _maxCount;

    public override void InteractAction(StackPresenter enteredStack)
    {
        Stackable stackable = _stackableProvider.InstantiateStackable();
        enteredStack.AddToStack(stackable);

        StackedInteraction?.Invoke();
        ItemGave?.Invoke(stackable);
    }
}