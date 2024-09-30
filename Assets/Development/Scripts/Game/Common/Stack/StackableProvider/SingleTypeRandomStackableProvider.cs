using System.Collections.Generic;
using UnityEngine;

public class SingleTypeRandomStackableProvider : SingleTypeStackableProvider
{
    [SerializeField] private StackableType _type;
    [SerializeField] private List<StackableType> _stackableList;

    public override StackableType Type => _type;

    private void OnValidate()
    {
        if (_stackableList == null)
            return;

        for (var i = 0; i < _stackableList.Count; i++)
            if (_stackableList[i] != _type)
            {
                Debug.LogWarning("Можно добавить только предмет с типом: " + Type);
                _stackableList[i] = StackableType.None;
            }
    }

    public override StackableType InstantiateStackable() =>
        GetStackable();
        //Instantiate(GetStackable(), transform);

    public override StackableType GetStackable() =>
        _stackableList[Random.Range(0, _stackableList.Count)];
}
