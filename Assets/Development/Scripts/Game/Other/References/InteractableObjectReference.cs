using UnityEngine;

public class InteractableObjectReference : MonoBehaviour
{
    [field: SerializeField] public CashDesk CashDesk { get; private set; }
    [field: SerializeField] public DeliveryTable DeliveryTable { get; private set; }
}
