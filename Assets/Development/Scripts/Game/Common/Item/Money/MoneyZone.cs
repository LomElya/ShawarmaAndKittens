using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyZone : MonoBehaviour
{
    [SerializeField] private MoneyStackHolder _moneyView;
    [SerializeField] private Money _defaultMoney;
    [SerializeField] private string _moneyZoneSavKey;
    [SerializeField] private Collider2D _collider;

    private List<Money> _moneys = new();

    public event System.Action Changed;
    public event System.Action Removed;

    public int Moneys => _moneys.Count;
    public int MoneysValue { get; private set; }

    private void OnDisable()
    {
        // PlayerPrefs.SetInt(_moneyZoneSavKey, _Moneys.Count);
    }

    public void Add(Money Money)
    {
        _moneys.Add(Money);
        _moneyView.Add(Money);

        MoneysValue += Money.Value;
        // PlayerPrefs.SetInt(_moneyZoneSavKey, _Moneys.Count);

        Changed?.Invoke();
    }

    public Money Remove()
    {
        if (_moneys.Count == 0)
            throw new System.InvalidOperationException("No money");

        Money lastMoney = _moneys[_moneys.Count - 1];

        _moneys.Remove(lastMoney);
        _moneyView.Remove(lastMoney);

        MoneysValue -= lastMoney.Value;
        //  PlayerPrefs.SetInt(_moneyZoneSavKey, _Moneys.Count);

        Changed?.Invoke();
        Removed?.Invoke();

        return lastMoney;
    }

    private IEnumerator SpawnMoney(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return null;
            Add(Instantiate(_defaultMoney, transform.position, Quaternion.identity));
        }
    }

    public void SetActive(bool active)
    {
        _collider.enabled = active;
    }
}
