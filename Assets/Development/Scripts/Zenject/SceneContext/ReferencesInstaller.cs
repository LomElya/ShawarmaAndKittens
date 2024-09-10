using System;
using UnityEngine;
using Zenject;

public class ReferencesInstaller : MonoInstaller
{
    [SerializeField] InteractableObjectReference _interactablesObject;
    [SerializeField] QueuesReferences _queuesReferences;

    public override void InstallBindings()
    {
        BindReferences();
    }

    private void BindReferences()
    {
        Container.Bind<InteractableObjectReference>().FromInstance(_interactablesObject).AsSingle();
        Container.Bind<QueuesReferences>().FromInstance(_queuesReferences).AsSingle();
    }
}
