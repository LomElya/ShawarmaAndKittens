using UnityEngine;

public class TimerInteractProduser : TimerInteractableZone
{
    [SerializeField] private InteractableObject _interactionObject;

    public override bool CanInteract(StackPresenter _) => _interactionObject.Active && !_interactionObject.Free;

    public override void InteractAction(StackPresenter enteredStack)
    {
        _interactionObject.CompliteInvoke();
    }

}
