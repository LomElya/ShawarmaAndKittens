using DG.Tweening;
using UnityEngine;

public abstract class AnimateButton : CustomButton
{
    [SerializeField] protected float _durationAnimation = 0.1f;
    [SerializeField] protected Ease _animateMode = Ease.Flash;

    protected Tweener _tween;

    protected override void onShow()
    {
        SetInteractable(false);
        KillTween();
        _tween = AnimateShow(OnCompliteAnimate);
    }

    protected override void onHide()
    {
        SetInteractable(false);
        KillTween();
        _tween = AnimateHide();
    }

    protected void OnCompliteAnimate() => SetInteractable(true);

    protected abstract Tweener AnimateShow(System.Action OnCompliteAnimation);
    protected abstract Tweener AnimateHide();

    private void KillTween()
    {
        if (_tween == null)
            return;

        if (_tween.active)
            _tween.Kill();
    }

    protected override void onDisable() => KillTween();
}
