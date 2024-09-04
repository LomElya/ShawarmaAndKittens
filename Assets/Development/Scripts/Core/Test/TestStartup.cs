using InputSystem;
using UnityEngine;
using Zenject;

public class TestStartup : MonoBehaviour
{
     [SerializeField] private MovableCharacter _playerMovement;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _input.Register(_playerMovement);
    }

    private void OnDestroy()
    {
        _input.Unregister();
    }
}
