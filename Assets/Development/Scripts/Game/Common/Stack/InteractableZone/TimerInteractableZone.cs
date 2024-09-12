using System;
using System.Collections;
using GameHandler;
using Modification;
using UnityEngine;
using Zenject;

public abstract class TimerInteractableZone : InteractableZoneBase, IModificationListener<float>
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _interactionTime;

    private Timer _timer = new Timer();
    private Coroutine _waitCoroutine;

    public ITimer Timer => _timer;

    protected virtual float InteracionTime => _interactionTime - _interactSpeedRate;

    private float _interactSpeedRate = 0;

    [Inject]
    private UpdateHandler _updateHandler;

    private void OnValidate() => _interactionTime = Mathf.Clamp(_interactionTime, 0f, float.MaxValue);

    private void OnUpdate() => _timer.Tick(Time.deltaTime);

    public override void Enabled()
    {
        base.Enabled();
        _updateHandler.AddUpdate(OnUpdate);
        _timer.Completed += OnTimeOver;
    }

    public override void Disabled()
    {
        base.Disabled();
        _updateHandler.RemoveUpdate(OnUpdate);
        _timer.Completed -= OnTimeOver;

        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);
    }

    public override void Entered(StackPresenter enteredStack)
    {
        if (CanInteract(enteredStack))
        {
            StartTime();
        }
        else
            _waitCoroutine = StartCoroutine(WaitUntilCanInteract(StartTime));
    }

    public override void Exited(StackPresenter otherStack)
    {
        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);

        _timer.Stop();
    }

    private void OnTimeOver()
    {
        InteractAction(EnteredStack);

        if (CanInteract(EnteredStack))
            _timer.Start(InteracionTime);
        else
            _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
    }

    private IEnumerator WaitUntilCanInteract(Action finalAction)
    {
        yield return new WaitUntil(() => CanInteract(EnteredStack));
        finalAction?.Invoke();
    }

    private void StartTime()
    {
        _timerView?.Init(_timer);
        _timer.Start(InteracionTime);
    }

    public abstract void InteractAction(StackPresenter enteredStack);
    public abstract bool CanInteract(StackPresenter enteredStack);

    public void OnModificationUpdate(float value)
    {
        _interactSpeedRate = value;
    }
}
