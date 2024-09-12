using UnityEngine;
using UnityEngine.UI;

public class StackInventoryView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _trashButton;

    private IconByStackable _iconByStackable;

    private void Start() => SetActive(false);

    public StackableType Type { get; private set; } = StackableType.None;
    public bool Empty => Type == StackableType.None;

    public void Init(IconByStackable iconByStackable)
    {
        _iconByStackable = iconByStackable;
        Type = StackableType.None;
    }

    public void Add(StackableType type)
    {
        Type = type;

        _image.sprite = _iconByStackable.GetIconByType(type);
        SetActive(true);
    }

    public void Remove()
    {
        Type = StackableType.None;

        SetActive(false);
        _image.sprite = null;
    }

    private void SetActive(bool active)
    {
        _image.gameObject.SetActive(active);
    }
}
