using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Create CustomersContent", fileName = "CustomersContent", order = 0)]
public class CustomersContent : ScriptableObject
{
    [SerializeField] private List<Pair> _customers;


    public Customer GetRandomCustomer()
    {
        Pair[] customers = _customers.ToArray();

        return customers.ElementAt(Random.Range(0, customers.Length)).Customer;
    }

    public Customer TryGetRandomCustomer(CustomerType customerType)
    {
        if (!TypeAvailable(customerType))
            return null;

        return GetRandomCustomer(customerType);
    }

    public Customer TryGetRandomCustomer(Func<CustomerType, bool> customerType)
    {
        IEnumerable<Pair> customers = _customers
            .Where(customer => customerType(customer.Customer.Type));

        return customers.ElementAt(Random.Range(0, customers.Count())).Customer;
    }

    private bool TypeAvailable(CustomerType customerType) =>
        _customers.Any(customer => customer.Customer.Type == customerType);


    private Customer GetRandomCustomer(CustomerType customerType)
    {
        Pair[] customers = _customers
            .Where(customer => customer.Customer.Type == customerType).ToArray();

        return customers.ElementAt(Random.Range(0, customers.Length)).Customer;
    }
}

[Serializable]
internal class Pair
{
    [field: SerializeField] public Customer Customer { get; private set; }
}