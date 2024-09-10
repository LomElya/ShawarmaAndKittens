using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMovementStatePreseter : MonoBehaviour
{
    [SerializeField] private AIMovement _parent;
    [SerializeField] private List<SequenceStateData> _sequencesState;

    private Dictionary<AIMovementStateType, SequenceStateData> _states = new();

    private AIMovementStates _currentState;

    public void Run()
    {
        SetState(GetFirstType());
    }

    private void SetState(AIMovementStateType stateType)
    {
        if (_currentState != null)
            ExitState();

        AIMovementStates newState;
        GetState(out newState, stateType);
        EnterState(newState);
    }

    private void GetState(out AIMovementStates newState, AIMovementStateType stateType)
    {
        if (_states.ContainsKey(stateType))
            newState = _states[stateType].Get();
        else
        {
            newState = null;

            AddStates(stateType, newState);
        }
    }

    private void EnterState(AIMovementStates newState)
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

    private void AddStates(AIMovementStateType stateType, AIMovementStates state)
    {
        SequenceStateData stateData = _sequencesState.FirstOrDefault(type => type.Type == stateType);
        _states.Add(stateType, stateData.Add(state));
    }

    private AIMovementStateType GetFirstType()
    {
        if (_sequencesState.Count > 0 || _sequencesState[0].Type == AIMovementStateType.None)
            return _sequencesState[0].Type;

        Debug.LogWarning("Не назначен ни один State. Выбран LeaveCafe");
        return AIMovementStateType.LeaveCafe;
    }

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
