using Action = System.Action;
using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    public event Action BecameFree;
    public event Action Complite;

    protected const float NewCustomerSetDelay = 1.0f;

    [SerializeField] protected Transform _customerWaitPoint;
    [SerializeField] private CustomerTrigger _customerTrigger;

    protected Customer _activeCustomer;

    public bool Active { get; private set; }
    public bool Free { get; private set; } = true;

    private void OnEnable() => _customerTrigger.Enter += OnCustomerEnter;
    private void Start() => SetActive(false);
    private void OnDisable() => _customerTrigger.Enter -= OnCustomerEnter;

    public Transform Wait(Customer customer)
    {
        _activeCustomer = customer;

        Free = false;
        OnEnter();

        return _customerWaitPoint;
    }

    public void Leave()
    {
        SetActive(false);
        OnLeave();
        BecameFree?.Invoke();
    }

    public void EndTransit() => Free = true;
    public void CompliteInvoke() => Complite?.Invoke();

    private IEnumerator NewCustomerActivationDelay()
    {
        yield return new WaitForSeconds(NewCustomerSetDelay);
        SetActive(true);
    }

    private void OnCustomerEnter(Customer customer)
    {
        if (customer != _activeCustomer)
            return;

        StartCoroutine(NewCustomerActivationDelay());
        onCustomerEnter(customer);
    }

    private void SetActive(bool active) => Active = active;

    protected IEnumerator ActivationDelay(Action actionActivation, float delay = NewCustomerSetDelay)
    {
        yield return new WaitForSeconds(delay);
        actionActivation?.Invoke();
    }

    protected virtual void onCustomerEnter(Customer customer) { }
    protected virtual void OnLeave() { }
    protected virtual void OnEnter() { }
}
