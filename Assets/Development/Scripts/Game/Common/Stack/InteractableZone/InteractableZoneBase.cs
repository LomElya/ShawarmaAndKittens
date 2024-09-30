using UnityEngine;

public class InteractableZoneBase : MonoBehaviour, IInteractableZone
{
    [SerializeField] private Trigger<Interactable> _trigger;

    protected Interactable EnteredInteractable;
    protected StackPresenter EnteredStack;

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
        _trigger.Stay += OnStay;
        _trigger.Exit += OnExit;

        Enabled();
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
        _trigger.Stay -= OnStay;
        _trigger.Exit -= OnExit;

        EnteredStack = null;
        Disabled();
    }

    private void OnEnter(Interactable enteredInteractable)
    {
        if (EnteredInteractable != null)
            return;

        EnteredInteractable = enteredInteractable;
        EnteredStack = enteredInteractable.StackPresenter;

        Entered(enteredInteractable);
    }

    private void OnStay(Interactable enteredInteractable)
    {
        if (EnteredInteractable != null)
            return;

        enteredInteractable.Enter(this);
        OnEnter(enteredInteractable);

        // if (EnteredStack == null)
        //     OnEnter(enteredStack);
    }

    private void OnExit(Interactable otherInteractable)
    {
        if (otherInteractable != EnteredInteractable)
            return;


        otherInteractable.Exit(this);
        Exited(otherInteractable);

        EnteredInteractable = null;
        EnteredStack = null;

        // if (otherStack == EnteredStack)
        // {
        //     Exited(otherStack);
        //     EnteredStack = null;
        // }
    }

    public virtual void Interact() { }
    public virtual void Entered(Interactable enteredInteractable) { }
    public virtual void Exited(Interactable otherInteractable) { }
    public virtual void Enabled() { }
    public virtual void Disabled() { }
}

public interface IInteractableZone
{
    void Interact();
}
