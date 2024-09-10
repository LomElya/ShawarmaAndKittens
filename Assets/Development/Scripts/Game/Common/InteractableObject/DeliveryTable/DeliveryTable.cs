using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryTable : InteractableObject
{
    [SerializeField] private MoneyZone _moneyZone;
    [SerializeField] private List<StackPresenter> _deliveryStacks;

    // public IEnumerable<StackableType> PurchaseTypes => _activeCustomer.PurchaseList.Items.Keys;
    // public PurchaseList PurchaseList => _activeCustomer.PurchaseList;

    public IEnumerable<Stackable> TakeAllItems()
    {
        IEnumerable<Stackable> stackables = new List<Stackable>();
        stackables = _deliveryStacks.Aggregate(stackables, (current, stack) => current.Concat(stack.RemoveAll()));

        //_activeCustomer.Pay(_moneyZone, _activeCustomer.PurchaseList.TotalPrice);
        _moneyZone.SetActive(false);
        StartCoroutine(ActivationDelay(ActiveMoneyZone));

        return stackables;
    }

    public void Deliver(StackableType type)
    {
        if (Active == false)
            throw new InvalidOperationException();

        //_activeCustomer.PurchaseList.Remove(type);
    }

    private IEnumerator MoneyZoneActivationDelay()
    {
        yield return new WaitForSeconds(NewCustomerSetDelay);
        _moneyZone.SetActive(true);
    }

    private void ActiveMoneyZone() => _moneyZone.SetActive(true);
}
