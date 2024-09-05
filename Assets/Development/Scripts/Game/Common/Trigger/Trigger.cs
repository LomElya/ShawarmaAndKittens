using System;
using System.Collections.Generic;
using GameHandler;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public abstract class Trigger<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool _disableStayCallback = false;

    public event Action<T> Enter;
    public event Action<T> Stay;
    public event Action<T> Exit;

    private Collider2D _collider2D;
    private List<KeyValuePair<Collider2D, T>> _enteredObjects;

    protected virtual int Layer => LayerMask.NameToLayer("Trigger");

    private UpdateHandler _updateHandler;
    private bool _injected = false;

    [Inject]
    private void Construct(UpdateHandler updateHandler)
    {
        _updateHandler = updateHandler;
        _injected = true;
    }

    private void Awake()
    {
        gameObject.layer = Layer;
        _enteredObjects = new List<KeyValuePair<Collider2D, T>>();

        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    private void Update()
    {
        if (_injected)
            return;

        OnUpdate();
    }

    private void OnUpdate()
    {
        for (int i = _enteredObjects.Count - 1; i >= 0; i--)
        {
            if (_enteredObjects[i].Key == null)
                _enteredObjects.RemoveAt(i);
            else if (_enteredObjects[i].Key.enabled == false)
                OnTriggerExit2D(_enteredObjects[i].Key);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            _enteredObjects.Add(new KeyValuePair<Collider2D, T>(other, triggered));
            Enter?.Invoke(triggered);

            OnEnter(triggered);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_disableStayCallback)
            return;

        if (other.TryGetComponent(out T triggered))
        {
            Stay?.Invoke(triggered);
            OnStay(triggered);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out T triggeredObject))
        {
            _enteredObjects.Remove(new KeyValuePair<Collider2D, T>(other, triggeredObject));
            Exit?.Invoke(triggeredObject);

            OnExit(triggeredObject);
        }
    }

    public void Enable()
    {
        _updateHandler.AddUpdate(OnUpdate);

        _collider2D.enabled = true;
        Enabled();
    }

    public void Disable()
    {
        _updateHandler.RemoveUpdate(OnUpdate);

        _collider2D.enabled = false;
        Disabled();

        foreach (var triggered in _enteredObjects)
            Exit?.Invoke(triggered.Value);

        _enteredObjects.Clear();
    }

    protected virtual void OnEnter(T triggered) { }
    protected virtual void OnStay(T triggered) { }
    protected virtual void OnExit(T triggered) { }
    protected virtual void Enabled() { }
    protected virtual void Disabled() { }
}
