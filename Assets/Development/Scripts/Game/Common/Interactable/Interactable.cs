using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private StackPresenter _stackPresenter;

    protected IInteractableZone _interactableZone;

    public StackPresenter StackPresenter => _stackPresenter;

    public void Enter(IInteractableZone interactableZone)
    {
        if (_interactableZone != null)
            return;

        _interactableZone = interactableZone;
        Entered(interactableZone);
    }

    public void Exit(IInteractableZone interactableZone)
    {
        if (_interactableZone == null && _interactableZone != interactableZone)
            return;

        _interactableZone = null;
        Entered(interactableZone);
    }

    protected abstract void Entered(IInteractableZone interactableZone);
    protected abstract void Exited(IInteractableZone interactableZone);
}

public interface IInteractable
{
    StackPresenter StackPresenter { get; }
    void Enter(IInteractableZone interactableZone);
    void Exit(IInteractableZone interactableZone);
}
