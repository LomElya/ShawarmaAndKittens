using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class CustomButton : MonoBehaviour
{
    public event System.Action OnClick;

    [SerializeField] private bool _interactable = true;
    [SerializeField] private bool _active = true;

    private Button _button;

    public bool Active { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        ChangeActive(_active);
        SetInteractable(_interactable);
        onAwake();
    }

    private void OnEnable()
    {
        Subscribe();
        onEnable();
    }

    private void OnDisable()
    {
        Unsubscribe();
        onDisable();
    }

    public void SetActive(bool active)
    {
        if (active)
            Show();
        else
            Hide();
    }

    public void Show()
    {
        ChangeActive(true);
        onShow();
    }

    public void Hide()
    {
        ChangeActive(false);
        onHide();
    }

    public void SetInteractable(bool interactable) => _button.interactable = interactable;
    public void Click() => OnButtonClick();

    private void ChangeActive(bool active)
    {
        Active = active;
        gameObject.SetActive(active);
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke();
        onButtonClick();
    }

    private void Subscribe() => _button.onClick.AddListener(OnButtonClick);
    private void Unsubscribe() => _button.onClick.RemoveListener(OnButtonClick);

    protected virtual void onAwake() { }
    protected virtual void onShow() { }
    protected virtual void onHide() { }
    protected virtual void onEnable() { }
    protected virtual void onDisable() { }
    protected virtual void onButtonClick() { }
    protected virtual void onSubscribe() { }
    protected virtual void onUnSubscribe() { }
}
