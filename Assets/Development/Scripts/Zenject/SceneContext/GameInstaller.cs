using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CustomerSpawner _customerSpawner;

    public override void InstallBindings()
    {
        BindSpawner();
        BindFabric();
    }

    private void BindSpawner()
    {
        Container.Bind<CustomerSpawner>().FromInstance(_customerSpawner).AsSingle();
    }

    private void BindFabric()
    {
        AIMovementStateFabric customerStateeFabric = new AIMovementStateFabric();
        Container.BindInstance(customerStateeFabric);
        Container.QueueForInject(customerStateeFabric);
    }
}
