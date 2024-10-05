using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class UIMenuView : MonoBehaviour
{
    [SerializeField] protected Button _outsideClickArea;

    protected UniTaskCompletionSource<bool> _taskCompletion;

    protected Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;
        OnAwake();
    }

    public async UniTask<bool> Show()
    {
        _taskCompletion = new UniTaskCompletionSource<bool>();

        OnShow();

        bool result = await _taskCompletion.Task;

        Unload();

        return result;
    }

    public void Hide()
    {
        _taskCompletion.TrySetResult(true);
        onHide();
    }

    private void Unload()
    {
        Unsubscribe();

        _canvas.enabled = false;
        onUnload();
    }

    private void OnShow()
    {
        Subscribe();

        _canvas.enabled = true;
        onShow();
    }

    private void Subscribe()
    {
        if (_outsideClickArea != null)
            _outsideClickArea.onClick.AddListener(Hide);

        OnSubscribe();
    }

    private void Unsubscribe()
    {
        if (_outsideClickArea != null)
            _outsideClickArea.onClick.RemoveListener(Hide);

        OnUnsubscribe();
    }


    protected virtual void OnAwake() { }
    protected virtual void onShow() { }
    protected virtual void onUnload() { }
    protected virtual void onHide() { }
    protected virtual void OnSubscribe() { }
    protected virtual void OnUnsubscribe() { }
}
