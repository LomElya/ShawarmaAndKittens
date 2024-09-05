using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class StackHolder : StackView
{
    [SerializeField] private float _offsetY;
    [SerializeField] private float _sortMoveDuration = 2f;

    private Vector2 _lastTopPosition;
    private int _lastChildCount;

    protected override Vector2 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        Vector2 stackableLocalScale = container.InverseTransformVector(stackable.lossyScale);
        Vector2 endPosition = new Vector2(0, stackableLocalScale.y / 2);

        if (container.childCount > _lastChildCount)
            endPosition += _lastTopPosition;
        else if (container.childCount != 0)
        {
            Transform topStackable = FindTopStackable(container);
            Vector2 topPosition = new Vector3(0, topStackable.localPosition.y + topStackable.localScale.y / 2, 0);

            endPosition += topPosition;
        }

        endPosition.y += _offsetY;

        _lastChildCount = container.childCount;
        _lastTopPosition = endPosition + new Vector2(0, stackableLocalScale.y / 2);

        return endPosition;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        IOrderedEnumerable<Transform> sortedList = unsortedTransforms.OrderBy(transform => transform.localPosition.y);
        Vector2 position = Vector2.zero;

        foreach (Transform item in sortedList)
        {
            position.y += item.localScale.y / 2;

            item.transform.DOComplete(true);
            item.transform.DOLocalMove(position, _sortMoveDuration);

            position.y += item.localScale.y / 2 + _offsetY;
        }
    }

    private Transform FindTopStackable(Transform container)
    {
        Transform topStackable = container.GetChild(0);

        foreach (Transform stackable in container)
            if (topStackable.position.y < stackable.position.y)
                topStackable = stackable;

        return topStackable;
    }
}
