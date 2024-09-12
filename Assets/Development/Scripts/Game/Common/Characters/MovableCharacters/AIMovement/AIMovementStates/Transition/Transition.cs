using System;

public class Transition
{
    private event Action OnEndTransit;

    public readonly AIMovementStateType _targetStateType;

    public Transition(AIMovementStateType targetStateType)
    {
        _targetStateType = targetStateType;
    }

    public AIMovementStateType TargetStateType => _targetStateType;


    public void Enable(Action endTransit)
    {
        OnEndTransit = endTransit;
        onEnable();
    }

    public void Disable() => onDisable();

    public void EndTransit() => OnEndTransit?.Invoke();

    protected virtual void onEnable() { }
    protected virtual void onDisable() { }
}
