using UnityEngine;

public class PlayerInteractable : Interactable
{
    [SerializeField] private CustomButton _button;

    private void Start()
    {
        _button.Hide();
    }

    protected override void Entered(IInteractableZone interactableZone)
    {
        _button.OnClick += OnClickButton;
        _button.Show();
    }

    protected override void Exited(IInteractableZone interactableZone)
    {
        _button.OnClick -= OnClickButton;
        _button.Hide();
    }

    private void OnClickButton()
    {
        if (_interactableZone == null)
            return;

        _interactableZone.Interact();
    }

    private void OnDestroy()
    {
        _button.OnClick -= OnClickButton;
    }
}
