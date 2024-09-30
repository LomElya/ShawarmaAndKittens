using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class StackView : StackViewBase
{
    // public event System.Action<StackableType> MoveEnded;

    [SerializeField] private Transform _stackContainer;
    [SerializeField] private float _animationDuration;
    [SerializeField] private FloatSetting _scalePunch = new FloatSetting(true, 1.1f);
    [SerializeField] private Vector2 _scaleMultiply = Vector3.one;

    private List<Transform> _transforms = new List<Transform>();

    protected override void onAdd(StackableType stackable) { }
    protected override void onRemove(StackableType stackable) { }

    public void Add(Stackable stackable)
    {
        Vector2 defaultScale = stackable.transform.localScale;

        stackable.transform.localScale = new Vector2(
            _scaleMultiply.x * defaultScale.x,
            _scaleMultiply.y * defaultScale.y);

        Vector2 endPosition = CalculateAddEndPosition(_stackContainer, stackable.transform);
        Vector2 endRotation = CalculateEndRotation(_stackContainer, stackable.transform);

        stackable.transform.DOComplete(true);
        stackable.transform.parent = _stackContainer;

        stackable.transform.DOLocalMove(endPosition, _animationDuration).OnComplete(() => InvokeMoveEnded(stackable.Type));
        stackable.transform.DOLocalRotate(endRotation, _animationDuration);

        if (_scalePunch.Enabled)
            stackable.transform.DOPunchScale(defaultScale * _scalePunch.Value, _animationDuration);

        _transforms.Add(stackable.transform);
        Add(stackable.Type);
        //Added?.Invoke(stackable);
    }

    public void Remove(Stackable stackable)
    {
        stackable.transform.DOComplete(true);
        stackable.transform.parent = null;

        int removedIndex = _transforms.IndexOf(stackable.transform);
        _transforms.RemoveAt(removedIndex);
        OnRemove(stackable.transform);

        Sort(_transforms);

        //Removed?.Invoke();
        Remove(stackable.Type);
    }

    public override float FindTopPositionY()
    {
        float topPositionY = 0f;

        foreach (Transform item in _transforms)
            if (item.position.y + item.localScale.y > topPositionY)
                topPositionY = item.position.y + item.localScale.y;

        return topPositionY;
    }

    protected abstract Vector2 CalculateAddEndPosition(Transform container, Transform stackable);
    protected abstract void Sort(List<Transform> unsortedTransforms);

    protected virtual void OnRemove(Transform stackable) { }
    protected virtual Vector2 CalculateEndRotation(Transform container, Transform stackable) => Vector2.zero;
}

[System.Serializable]
public class Setting<T>
{
    [SerializeField] private bool _enabled;
    [SerializeField] private T _value;

    public Setting(bool enabled, T value)
    {
        _enabled = enabled;
        _value = value;
    }

    public bool Enabled => _enabled;
    public T Value => _value;
}

[System.Serializable]
public class FloatSetting : Setting<float>
{
    public FloatSetting(bool enabled, float value) : base(enabled, value) { }
}