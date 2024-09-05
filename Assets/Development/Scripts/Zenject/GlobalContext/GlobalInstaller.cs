using UnityEngine;
using GameHandler;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private UpdateHandler _updateHandler;

    public override void InstallBindings()
    {
        BindServices();
    }

    private void BindServices()
    {
        Container.Bind<Wallet>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpdateHandler>().FromInstance(_updateHandler).AsSingle();
    }
}
