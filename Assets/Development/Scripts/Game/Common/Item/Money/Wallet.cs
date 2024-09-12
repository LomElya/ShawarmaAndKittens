using System;

public class Wallet
{
    public event Action<int> BalanceChanged;

    public int ValueMoney = 10;
    public bool HasMoney => ValueMoney > 0;

    public void AddMoney(int value)
    {
        ValueMoney += value;
        OnMoneyChanged();
    }

    public void SpendMoney(int value)
    {
        if (!HasMoney && !IsEnough(value))
            return;

        ValueMoney -= value;
        OnMoneyChanged();
    }

    public bool IsEnough(int value) => ValueMoney >= value;

    private void OnMoneyChanged()
    {
        BalanceChanged?.Invoke(ValueMoney);
        //_money.Save();
    }
}
