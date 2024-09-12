using System;
using UnityEngine;
using Zenject;

public class ReferencesInstaller : MonoInstaller
{
    [SerializeField] InteractableObjectReference _interactablesObject;
    [SerializeField] QueuesReferences _queuesReferences;
    [SerializeField] private PurchaseListFactory _purchaseListFactory;

    public override void InstallBindings()
    {
        BindReferences();

        Container.Bind<PurchaseListFactory>().FromInstance(_purchaseListFactory).AsSingle();
    }

    private void BindReferences()
    {
        Container.Bind<InteractableObjectReference>().FromInstance(_interactablesObject).AsSingle();
        Container.Bind<QueuesReferences>().FromInstance(_queuesReferences).AsSingle();
    }
}
