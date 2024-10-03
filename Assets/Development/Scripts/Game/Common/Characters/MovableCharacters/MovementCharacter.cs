using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementCharacter : Character, IMovable
{
    [SerializeField] protected float _speed;

    protected Rigidbody2D _rigidbody;
    protected float _speedRate = 1f;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        OnAwake();
    }

    public void Move(Vector2 direction)
    {
        ChangeMove(true);
        OnMove(direction);
        _view.Move(direction);
        _view.Flip(direction);
    }

    public void Stop()
    {
        ChangeMove(false);
        OnStopMove();
    }

    protected abstract void OnMove(Vector2 direction);
    protected abstract void OnStopMove();


    protected virtual void OnAwake() { }
    protected virtual void ChangeMove(bool isMoving)
    {
        IsMoving = isMoving;
        _view.SetMove(isMoving);
    }
}
