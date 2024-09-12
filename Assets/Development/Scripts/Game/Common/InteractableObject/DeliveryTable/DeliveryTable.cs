using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryTable : InteractableObject
{
    [SerializeField] private MoneyZone _moneyZone;
    [SerializeField] private List<StackPresenter> _deliveryStacks;
    [SerializeField] private PurchaseListView _purchaseListView;

    public PurchaseList PurchaseList => _activeCustomer.PurchaseList;
    public IEnumerable<StackableType> PurchaseTypes => PurchaseList.Items.Keys;

    public IEnumerable<StackPresenter> DeliveryStacks => _deliveryStacks;

    public IEnumerable<Stackable> TakeAllItems()
    {
        IEnumerable<Stackable> stackables = new List<Stackable>();
        stackables = _deliveryStacks.Aggregate(stackables, (current, stack) => current.Concat(stack.RemoveAll()));

        _activeCustomer.Pay(_moneyZone, PurchaseList.TotalPrice);
        _moneyZone.SetActive(false);
        StartCoroutine(ActivationDelay(ActiveMoneyZone));

        return stackables;
    }

    public void Deliver(StackableType type)
    {
        if (Active == false)
            throw new InvalidOperationException();

        PurchaseList.Remove(type);
        _purchaseListView.UpdateView();
    }

    protected override void onCustomerEnter(Customer customer) => _purchaseListView.Init(PurchaseList);

    private void ActiveMoneyZone() => _moneyZone.SetActive(true);
}
