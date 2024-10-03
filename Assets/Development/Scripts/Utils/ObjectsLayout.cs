using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class ObjectsLayout : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private float _space;

    private int _childCount;

#if UNITY_EDITOR
    private void OnValidate() => UpdatePositions();
#endif

    private void OnEnable()
    {
        _childCount = transform.childCount;
#if UNITY_EDITOR
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
#endif
    }

    public void OnChange() => OnHierarchyChanged();

    private void OnHierarchyChanged()
    {
        if (transform.childCount == _childCount)
            return;

        _childCount = transform.childCount;
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = position;

            if (_type == Type.Horizontal)
                position += Vector3.right * _space;
            if (_type == Type.Vertical)
                position += Vector3.down * _space;
        }

        EditorUtility.SetDirty(this);
    }

    private enum Type
    {
        Horizontal,
        Vertical
    }
}
