using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoneyMagnit : MonoBehaviour
{
    [SerializeField] private float _attractDuration = 1f;
    [SerializeField] private float _followOffsetDistance = 5f;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _scaleReduceDuration = 0.5f;
    [SerializeField] private float _scaleReduceMoveSpeed = 5;
    [SerializeField] private float _takeShakeDuration = 0.2f;

    private float _followRange => _followOffsetDistance * _followOffsetDistance;

    public event Action<int> Attracted;

    public void AttractLinear(Money money)
    {
        money.transform.DOComplete(true);

        money.transform.DOLocalRotate(Vector3.zero, 0.05f);
        money.transform.DOLocalMove(transform.position, 0.05f).OnComplete(() =>
        {
            Attracted?.Invoke(money.Value);
            Destroy(money.gameObject);
        });
    }

    public void Attract(Money money)
    {
        money.DisableCollision();
        StartCoroutine(Animate(money));
    }

    private IEnumerator Animate(Money money)
    {
        money.transform.DOComplete(true);
        money.transform.DOShakeScale(_takeShakeDuration, 1f);

        yield return new WaitForSeconds(_takeShakeDuration);

        money.transform.DOLocalRotate(Vector3.zero, _attractDuration);

        while (Vector3.SqrMagnitude(transform.position - money.transform.position) > _followRange)
        {
            float clampedSpeed = Mathf.Clamp(_speed * Time.deltaTime, 0, 1);
            money.transform.position = Vector3.Lerp(money.transform.position, transform.position, clampedSpeed);

            yield return null;
        }

        money.transform.DOScale(0, _scaleReduceDuration).OnComplete(() =>
        {
            Attracted?.Invoke(money.Value);
            money.transform.DOComplete(true);
            Destroy(money.gameObject);
        });

        while (money)
        {
            money.transform.position = Vector3.MoveTowards(money.transform.position, transform.position,
                _scaleReduceMoveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}