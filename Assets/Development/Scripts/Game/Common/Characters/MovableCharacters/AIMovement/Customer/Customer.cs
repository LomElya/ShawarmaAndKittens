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

    private PurchaseListFactory _purchaseListFactory;

    public PurchaseList PurchaseList { get; private set; }
    public CustomerType Type => _type;
    private UpdateHandler _updateHandler;
    public StackPresenter Stack => _stack;

    [Inject]
    private void Construct(UpdateHandler updateHandler, PurchaseListFactory purchaseListFactory)
    {
        _updateHandler = updateHandler;
        _purchaseListFactory = purchaseListFactory;
    }

    public void Init()
    {
        PurchaseList = _purchaseListFactory.Create();
        Initialized();
        _updateHandler.AddUpdate(OnUpdate);
    }

    public void CreateNewPurchaseList(StackableType type) => CreateNewPurchaseList(Stack.CalculateCount(type));
    public void CreateNewPurchaseList() => CreateNewPurchaseList(Stack.CalculateCount(StackableType.Shawarma) + 1);
    public void CreateNewPurchaseList(int count) => PurchaseList = _purchaseListFactory.Create(count);

    public void Pay(MoneyZone moneyZone, int totalPrice)
    {
        if (gameObject.activeInHierarchy)
            _moneyPayer.Pay(moneyZone, totalPrice);
    }

    public void Leave() => Left?.Invoke(this);
    private void OnDestroy() => _updateHandler.RemoveUpdate(OnUpdate);
}
