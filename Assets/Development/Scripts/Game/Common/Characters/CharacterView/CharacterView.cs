using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private SpriteConfig _config;
    [SerializeField] private float _frameDelay = 0.2f;

    private int _spriteIndex;
    private float _curFrameDelay;
    private Vector3 _prevDirection;

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
        // if (_animator)
        //     _animator.SetBool(isRun, moving);

        if (!moving)
            SetIdleSprite();
    }

    public void Move(Vector3 direction)
    {
        _curFrameDelay += Time.deltaTime;

        if (_curFrameDelay < _frameDelay)
            return;

        _curFrameDelay = 0f;
        if (direction.x != 0 && direction.y != 0)
            _prevDirection = direction;

        // // Идём вправо
        // if (direction.x > 0)
        // {
        //     SetSprite(_config.RightSpritePack);
        // }
        // // Идём влево
        // else if (direction.x < 0)
        // {
        //     SetSprite(_config.LeftSpritePack);
        // }

        // Идём вверх
        if (direction.y > 0)
            SetSprite(_config.UpSpritePack);
        // Идём вниз
        else if (direction.y < 0)
            SetSprite(_config.DownSpritePack);

        if (direction.x == 0 && direction.y == 0)
            SetIdleSprite();
    }

    private void SetSprite(SpritePack playerSpritePack)
    {
        if (_spriteIndex >= playerSpritePack.SpriteSequence.Count)
            _spriteIndex = 0;

        _spriteRenderer.sprite = playerSpritePack.SpriteSequence[_spriteIndex];
        _spriteIndex++;
    }

    private void SetIdleSprite()
    {
        // if (_prevDirection.x > 0)
        // {
        //     _spriteRenderer.sprite = _config.RightSpritePack.IdleSprite;
        // }
        // else if (_prevDirection.x < 0)
        // {
        //     _spriteRenderer.sprite = _config.LeftSpritePack.IdleSprite;
        // }
        if (_prevDirection.y > 0)
            _spriteRenderer.sprite = _config.UpSpritePack.IdleSprite;
        else if (_prevDirection.y < 0)
            _spriteRenderer.sprite = _config.DownSpritePack.IdleSprite;
    }

    public void Flip(Vector3 direction)
    {
        //CheckFlip(direction);

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
