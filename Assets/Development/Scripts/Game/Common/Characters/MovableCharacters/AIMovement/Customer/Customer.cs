using System;
using GameHandler;
using UnityEngine;
using Zenject;

public class Customer : AIMovement
{
    public event Action<Customer> Left;

    [SerializeField] private MoneyPayer _moneyPayer;
    [SerializeField] private StackPresenter _stack;
    [SerializeField] private CustomerType _type;

    // public PurchaseList PurchaseList { get; private set; }
    public CustomerType Type => _type;

    private UpdateHandler _updateHandler;

    public StackPresenter Stack => _stack;

    [Inject]
    private void Construct(UpdateHandler updateHandler)
    {
        _updateHandler = updateHandler;
    }

    public void Init()
    {
        Initialized();
        _updateHandler.AddUpdate(OnUpdate);
    }

    public void Pay(MoneyZone moneyZone, int totalPrice)
    {
        if (gameObject.activeInHierarchy)
            _moneyPayer.Pay(moneyZone, totalPrice);
    }

    public void Leave() =>
        Left?.Invoke(this);

    private void OnDestroy()
    {
        _updateHandler.RemoveUpdate(OnUpdate);
    }
}
