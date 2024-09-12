using InputSystem;
using UnityEngine;
using Zenject;

public class TestStartup : MonoBehaviour
{
    [SerializeField] private MovementCharacter _playerMovement;

    [Space]
    [SerializeField] private PlayerStackPresenter _stackPresenter;
    [SerializeField] private StackInventoryList _stackInventoryList;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _stackInventoryList.Init(_stackPresenter);
        _input.Register(_playerMovement);
    }

    private void OnDestroy()
    {
        _input.Unregister();
    }
}
