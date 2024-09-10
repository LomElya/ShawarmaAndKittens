using UnityEngine;

public class SquareZone : WorldZone
{
    [SerializeField] private Vector2 _size;

    public override Vector2 GetPoint()
    {
        var x = Random.Range(-_size.x / 2, _size.x / 2);
        var y = Random.Range(-_size.y / 2, _size.y / 2);

        return transform.position + new Vector3(x, y, 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(_size.x, _size.y));
    }
#endif
}
