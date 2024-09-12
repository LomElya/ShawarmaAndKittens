using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class DropableItem<TStackable> : Item where TStackable : Stackable
{
    [SerializeField] private float _horizontalForce = 50;
    [SerializeField] protected float _canTakeDelay;
    [SerializeField] protected TStackable _stackable;

    protected bool _canTake;
    protected bool _taken;

    public TStackable Stackable => _stackable;
    public bool CanTake => !_taken && _canTake;

    private Rigidbody2D _rigidbody;

    protected override void OnAwake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(UnlockTake());
    }

    public TStackable Take()
    {
        if (!CanTake)
            throw new InvalidOperationException();

        _taken = true;
        DisableGravity();

        return _stackable;
    }

    public void Push()
    {
        Vector2 random = Random.insideUnitCircle;
        Vector2 shift = new Vector3(random.x, random.y);
        Push(Vector2.up + shift * _horizontalForce);
    }

    public void Push(Vector2 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Force);
        StartCoroutine(DisableBodyWhenReady());
    }

    public void DisableGravity()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    public IEnumerator UnlockTake()
    {
        yield return new WaitForSeconds(_canTakeDelay);
        _canTake = true;
    }

    private IEnumerator DisableBodyWhenReady()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => _rigidbody.velocity == Vector2.zero);

        _rigidbody.isKinematic = true;
    }
}