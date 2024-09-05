using UnityEngine;

public class Money : Stackable
{
    [SerializeField] private int _value = 1;
    [SerializeField] private Collider2D _collider;

    public override StackableType Type => StackableType.Money;
    public int Value => _value;

    private void Awake()
    {
        _collider ??= GetComponent<Collider2D>();

        if (_collider == null)
            _collider = gameObject.AddComponent<BoxCollider2D>();
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }
}
