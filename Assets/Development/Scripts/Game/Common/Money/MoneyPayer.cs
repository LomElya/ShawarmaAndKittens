using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MoneyPayer : MonoBehaviour
{
    [SerializeField] private int _dollars;
    [SerializeField] private DroppableMoney _moneyPrefab;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0.38f, 2.2f, 0.0f);
    [SerializeField] private float _payDelay = 1.5f;

    [Header("Тестовый вариант, удалить потом")]
    [SerializeField] private MoneyZone _moneyZone;

    private Action _payCompleted;
    private int _defaultDollars;
    private float _force = 6f;
    private float _spawnDelayBetweenDollars = 0.1f;
    private Vector3 _spawnPosition => transform.position + _spawnOffset + Random.insideUnitSphere * 0.5f;

    public event Action PayCompleted;

    public bool DollarsMultiplied => _dollars != _defaultDollars;

    private void Awake()
    {
        _defaultDollars = _dollars;
        StartCoroutine(TestPayMoney());
    }

    private IEnumerator TestPayMoney()
    {
        yield return new WaitForSeconds(0.5f);

        Pay(_moneyZone, _dollars);
    }

    public void MultiplyDollars()
    {

        if (DollarsMultiplied)
            throw new InvalidOperationException("Already multiplied");

        _dollars *= 2;
    }

    public void ReturnDefaultDollars()
    {
        _dollars = _defaultDollars;
    }

    public MoneyPayer Pay(MoneyZone moneyZone, int amount)
    {
        if (gameObject.activeSelf)
            StartCoroutine(CreateMoneys(moneyZone, amount));

        return this;
    }

    public MoneyPayer Pay(int multiplier = 1)
    {
        StartCoroutine(CreateMoneys(multiplier * _dollars));

        return this;
    }

    public void OnPayCompleted(Action payCompleted)
    {
        _payCompleted = payCompleted;
    }

    private IEnumerator CreateMoneys(MoneyZone moneyZone, int count)
    {
        for (int i = 0; i < count; i++)
        {
            DroppableMoney spawnedDollar = Instantiate(_moneyPrefab, transform.position, Quaternion.identity);
            spawnedDollar.DisableGravity();
            moneyZone.Add(spawnedDollar.Money);

            yield return null;
        }

        _payCompleted?.Invoke();
        _payCompleted = null;
    }

    private IEnumerator CreateMoneys(int count)
    {
        if (_payDelay != 0)
            yield return new WaitForSeconds(_payDelay);

        for (int i = 0; i < count; i++)
        {
            DroppableMoney spawnedDollar = Instantiate(_moneyPrefab, _spawnPosition, Random.rotation);
            spawnedDollar.Push(GetRandomDirection() * _force);

            yield return new WaitForSeconds(_spawnDelayBetweenDollars);
        }

        _payCompleted?.Invoke();
        PayCompleted?.Invoke();
        _payCompleted = null;
    }

    private Vector3 GetRandomDirection()
    {
        var direction = Random.insideUnitSphere;
        direction.y = Mathf.Abs(direction.y);
        direction.y += 10f;

        return direction.normalized;
    }
}