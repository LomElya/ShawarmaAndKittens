using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class CharacterView : MonoBehaviour
{
    protected Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public const string isRun = nameof(isRun);
    public const string isDig = nameof(isDig);

    private bool _faceRight = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        OnAwake();
    }

    public void SetMove(bool moving)
    {
        if (_animator)
            _animator.SetBool(isRun, moving);
    }

    public void Flip(Vector3 direction)
    {
        CheckFlip(direction);

        // Transform transformModel = _spriteRenderer.transform;
        // transformModel.LookAt(transformModel.position + direction);
        //_spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private void CheckFlip(Vector3 direction)
    {
        if (!_faceRight && direction.x > 0.01f)
            onFlip();
        if (_faceRight && direction.x < -0.01f)
            onFlip();
    }

    private void onFlip()
    {
        _faceRight = !_faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void StartDigging() => SetDigging(true);
    public void StopDigging() => SetDigging(false);
    public void SetDigging(bool isDigging) => _animator.SetBool(isDig, isDigging);

    public void StopHolding() => _animator.SetLayerWeight(1, 0f);
    private void Hold() => _animator.SetLayerWeight(1, 1f);

    private void OnAdded(Stackable _) => Hold();
    private void OnBecameEmpty() => StopHolding();

    protected virtual void OnAwake() { }
}
