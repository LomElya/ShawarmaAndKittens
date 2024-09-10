using UnityEngine;

[System.Serializable]
public class SequenceStateData
{
    [SerializeField] private AIMovementStateType _stateType;
    [SerializeField] private AIMovementStateType _targerStateType;

    public AIMovementStateType Type => _stateType;
    public AIMovementStateType TargetState => _targerStateType;

    private AIMovementState _state;

    public SequenceStateData Add(AIMovementState state)
    {
        _state = state;
        return this;
    }
    public AIMovementState Get() => _state;
}
