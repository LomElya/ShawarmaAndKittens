using UnityEngine;

[System.Serializable]
public class SequenceStateData
{
    [SerializeField] private AIMovementStateType _stateType;

    public AIMovementStateType Type => _stateType;

    private AIMovementStates _state;

    public SequenceStateData Add(AIMovementStates state)
    {
        _state = state;
        return this;
    }
    public AIMovementStates Get() => _state;
}
