using UnityEngine;

public abstract class Stackable : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    public abstract StackableType Type { get; }

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
