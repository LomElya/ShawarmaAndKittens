using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StackInventoryView : MonoBehaviour, IPointerDownHandler
{
    public event System.Action<StackableType> RemoveStot;
    public event System.Action<StackInventoryView> OnSelected;

    [SerializeField] private Image _image;
    [SerializeField] private Image _selectedImage;
    [SerializeField] private CustomButton _trashButton;

    private IconByStackable _iconByStackable;

    public bool Selected { get; private set; } = false;

    private void Start() => SetActive(false);
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    public StackableType Type { get; private set; } = StackableType.None;
    public bool Empty => Type == StackableType.None;

    public void Init(IconByStackable iconByStackable)
    {
        _iconByStackable = iconByStackable;
        Type = StackableType.None;
        Unselect();
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
        Unselect();
    }

    public void Select()
    {
        if (Empty)
            return;

        OnSelected?.Invoke(this);
        ChangeSelected(true);
    }

    public void Unselect()
    {
        ChangeSelected(false);
    }

    private void SetActive(bool active)
    {
        _image.gameObject.SetActive(active);
    }

    private void ChangeSelected(bool selected)
    {
        Selected = selected;
        _trashButton.SetActive(selected);
        //_selectedImage.gameObject.SetActive(selected);
    }

    private void RemoveSlot() => RemoveStot?.Invoke(Type);
    private void Subscribe() => _trashButton.OnClick += RemoveSlot;
    private void Unsubscribe() => _trashButton.OnClick -= RemoveSlot;

    public void OnPointerDown(PointerEventData eventData) => Select();
}
