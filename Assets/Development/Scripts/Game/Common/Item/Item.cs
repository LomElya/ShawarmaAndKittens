using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;

    private void Awake()
        => OnAwake();

    protected virtual void OnAwake() { }
}
