using UnityEngine;

public class PlayerMovement : MovementCharacter
{
    protected override void OnMove(Vector2 direction)
    {
        if (_rigidbody != null)
            _rigidbody.velocity = direction * _speed * _speedRate;
    }

    protected override void OnStopMove()
    {
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;
    }
}
