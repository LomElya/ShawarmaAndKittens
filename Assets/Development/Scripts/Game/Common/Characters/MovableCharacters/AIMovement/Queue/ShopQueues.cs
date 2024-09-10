using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopQueues : Queue<AIMovementQueue>
{
    [SerializeField] private new List<AIMovementQueue> _queues = new();

    public override bool NotFull => _queues?.FirstOrDefault(queue => queue.NotFull) != null;
    public override bool Empty => _queues?.FirstOrDefault(queue => !queue.Empty) == null;
}
