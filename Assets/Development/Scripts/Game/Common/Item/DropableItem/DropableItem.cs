using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class DropableItem : Item
{
    [SerializeField] private float _horizontalForce = 50;

    private Rigidbody2D _rigidbody;

    protected override void OnAwake() => 
        _rigidbody = GetComponent<Rigidbody2D>();

    public void Push()
    {
        Vector2 random = Random.insideUnitCircle;
        Vector3 shift = new Vector3(random.x, 0, random.y);
        Push(Vector3.up + shift * _horizontalForce);
    }

    public void Push(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Force);
        StartCoroutine(DisableBodyWhenReady());
    }

    public void DisableGravity()
    {
        _rigidbody.isKinematic = true;
        //_rigidbody.useGravity = false;
        _collider.enabled = false;
    }

    private IEnumerator DisableBodyWhenReady()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => _rigidbody.velocity == Vector2.zero);

        _rigidbody.isKinematic = true;
    }
}