using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private string _template;
    [SerializeField] private TMP_Text _text;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnEnable()
    {
        if (_wallet == null)
            return;

        _wallet.BalanceChanged += UpdateValue;

        UpdateValue(_wallet.ValueMoney);
    }

    private void UpdateValue(int amount) => _text.text = $"{_template}{amount}";

    private void OnDisable() => _wallet.BalanceChanged -= UpdateValue;
}
