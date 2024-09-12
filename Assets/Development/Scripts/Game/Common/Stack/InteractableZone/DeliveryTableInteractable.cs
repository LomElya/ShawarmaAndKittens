using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryTableInteractable : TimerInteractableZone
{
    [SerializeField] private DeliveryTable _deliveryTable;

    private IEnumerable<StackPresenter> _stacks => _deliveryTable.DeliveryStacks;

    public override void InteractAction(StackPresenter enteredStack)
    {
        foreach (StackableType type in _deliveryTable.PurchaseTypes)
        {
            if (CanInteract(enteredStack, type))
            {
                Stackable stackable = enteredStack.RemoveFromStack(type);

                foreach (var stack in _stacks.Where(stack => CanAddToStack(type, stack)))
                {
                    if (stack.CanRemoveFromStack(type))
                        continue;

                    stack.AddToStack(stackable);
                    _deliveryTable.Deliver(type);

                    return;
                }

                return;
            }
        }
    }

    private Stackable TryRemoveStackable(StackPresenter enteredStack, StackableType type)
    {
        Stackable stackable = null;

        if (CanInteract(enteredStack, type))
            stackable = enteredStack.RemoveFromStack(type);

        return stackable;
    }

    public override bool CanInteract(StackPresenter enteredStack) =>
      _deliveryTable.Active && _deliveryTable.PurchaseTypes.Any(type => CanInteract(enteredStack, type));

    private bool CanInteract(StackPresenter enteredStack, StackableType type) =>
        _deliveryTable.Active && enteredStack.CanRemoveFromStack(type) && CanAddToStack(type);

    private bool CanAddToStack(StackableType type)
    {
        foreach (var stack in _stacks)
        {
            if (CanAddToStack(type, stack))
                return true;
        }

        return false;
    }

    private bool CanAddToStack(StackableType type, StackPresenter stack)
    {
        if (stack.CanAddToStack(type) && stack.CanRemoveFromStack(type) == false)

            return true;

        return false;
    }
}
