using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovableCharacter : Character, IMovable
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private float _speedRate = 1f;

    public bool IsMoving { get; private set; }

    private void Awake()
        => _rigidbody = GetComponent<Rigidbody2D>();

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed * _speedRate;

        _view.Flip(direction);
        ChangeMove(true);
    }

    public void Stop()
    {
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;

        ChangeMove(false);
    }

    private void ChangeMove(bool isMoving)
    {
        IsMoving = isMoving;
        _view.SetMove(IsMoving);
    }
}
