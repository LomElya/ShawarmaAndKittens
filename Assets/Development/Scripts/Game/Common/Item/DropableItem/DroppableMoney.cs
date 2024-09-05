using System;
using System.Collections;
using UnityEngine;

public class DroppableMoney : DropableItem
{
    [SerializeField] private float _canTakeDelay;
    [SerializeField] private Money _money;

    private bool _canTake;
    private bool _taken;


    public Money Money => _money;
    public bool CanTake => !_taken && _canTake;

    protected override void OnAwake()
    {
        base.OnAwake();
        StartCoroutine(UnlockTake());
    }

    public IEnumerator UnlockTake()
    {
        yield return new WaitForSeconds(_canTakeDelay);
        _canTake = true;
    }

    public Money Take()
    {
        if (!CanTake)
            throw new InvalidOperationException();

        _taken = true;
        DisableGravity();

        return _money;
    }
}