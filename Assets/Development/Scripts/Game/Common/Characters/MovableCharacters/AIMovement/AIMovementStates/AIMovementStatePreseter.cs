using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AIMovementStatePreseter : MonoBehaviour
{
    [SerializeField] private AIMovement _parent;
    [SerializeField] private List<SequenceStateData> _sequencesState;

    private Dictionary<AIMovementStateType, SequenceStateData> _states = new();

    private AIMovementState _currentState;

    protected AIMovementStateFabric _fabric;

    [Inject]
    private void Construct(AIMovementStateFabric stateFabric)
    {
        _fabric = stateFabric;
    }

    private void Run()
    {
        SetState(GetFirstType());
    }

    private void SetState(AIMovementStateType stateType)
    {
        if (_currentState != null)
            ExitState();

        AIMovementState newState;
        GetState(out newState, stateType);
        EnterState(newState);
    }

    private void GetState(out AIMovementState newState, AIMovementStateType stateType)
    {
        if (_states.ContainsKey(stateType))
            newState = _states[stateType].Get();
        else
        {
            Debug.Log(_fabric);
            SequenceStateData stateData = GetData(stateType);
            newState = _fabric.CreateState(stateData.Type, stateData.TargetState, _parent);
            stateData.Add(newState);

            AddStates(stateType, stateData);
        }
    }

    private void EnterState(AIMovementState newState)
    {
        _currentState = newState;
        _currentState.ChangeState += SetState;
        _currentState.Enter();
    }

    private void ExitState()
    {
        _currentState.ChangeState -= SetState;
        _currentState.Exit();
    }

    private void AddStates(AIMovementStateType stateType, SequenceStateData stateData)
    {
        _states.Add(stateType, stateData);
    }

    private AIMovementStateType GetFirstType()
    {
        if (_sequencesState.Count > 0 || _sequencesState[0].Type == AIMovementStateType.None)
            return _sequencesState[0].Type;

        Debug.LogWarning("Не назначен ни один State. Выбран LeaveCafe");
        return AIMovementStateType.LeaveCafe;
    }

    private SequenceStateData GetData(AIMovementStateType stateType) => _sequencesState.FirstOrDefault(type => type.Type == stateType);

    private void OnEnable()
    {
        _parent.OnInitialized += Run;

        if (_currentState != null)
            _currentState.ChangeState += SetState;
    }

    private void OnDisable()
    {
        _parent.OnInitialized -= Run;

        if (_currentState != null)
            _currentState.ChangeState -= SetState;
    }
}
