using UnityEngine;

public class QueuesReferences : MonoBehaviour
{
    [field: SerializeField] public ShopQueues CashDeskQueues { get; private set; }
    [field: SerializeField] public ShopQueues DeliveryQueues { get; private set; }
}
