using DG.Tweening;
using UnityEngine;

public class MoveAnimateButton : AnimateButton
{
    [SerializeField] private RectTransform _startPosition;
    [SerializeField] private RectTransform _endPosition;

    protected override Tweener AnimateShow(System.Action OnCompliteAnimation) =>
        Move(_endPosition.position).OnComplete(() => OnCompliteAnimation?.Invoke());

    protected override Tweener AnimateHide() =>
        Move(_startPosition.position);

    private Tweener Move(Vector2 targetPosition) => transform.DOMove(targetPosition, _durationAnimation).SetEase(_animateMode);
}
