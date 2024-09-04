using InputSystem;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private JoystickInput _mobileInput;

    public override void InstallBindings()
    {
        BindInput();
    }

    private void BindInput()
    {
        MobileInput();
    }

    private void MobileInput()
    {
        if (_mobileInput == null)
            return;
        //Container.BindInterfacesAndSelfTo<DesctopInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<JoystickInput>().FromInstance(_mobileInput).AsSingle();
    }
}
