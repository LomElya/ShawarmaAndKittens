using System;
using UnityEngine;
using Zenject;

public class MoneyCollector : MonoBehaviour
{
    public event Action<int> Collected;

    [SerializeField] private MoneyMagnit _magnit;
   // [SerializeField] private MoneyHolder _moneyHolder;
    [SerializeField] private Trigger<DroppableMoney> _trigger;
    [SerializeField] private Trigger<MoneyZone> _moneyZoneTrigger;

    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnEnable()
    {
        _magnit.Attracted += OnDollarAttracted;
        _trigger.Stay += OnStay;
        _moneyZoneTrigger.Stay += OnStay;
    }

    private void OnDisable()
    {
        _magnit.Attracted -= OnDollarAttracted;
        _trigger.Stay -= OnStay;
        _moneyZoneTrigger.Stay -= OnStay;
    }

    private void OnStay(DroppableMoney droppableMoney)
    {
        if (droppableMoney.CanTake == false)
            return;

        Money dollar = droppableMoney.Take();
        _magnit.Attract(dollar);
    }

    private void OnStay(MoneyZone moneyZone)
    {
        if (moneyZone.Moneys == 0)
            return;

        for (int i = 0; i < 5; i++)
        {
            Money money = moneyZone.Remove();
            _magnit.Attract(money);

            if (moneyZone.Moneys == 0)
                break;
        }
    }

    private void OnDollarAttracted(int dollarValue)
    {
        _wallet.AddMoney(dollarValue);
        Collected?.Invoke(dollarValue);
    }
}