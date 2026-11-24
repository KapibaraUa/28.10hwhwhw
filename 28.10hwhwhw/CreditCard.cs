using System;

namespace HomeworkEventsAndExtensions;

public delegate void CardEventHandler(string message);

internal class CreditCard
{
    private string _cardNumber = string.Empty;
    private string _ownerName = string.Empty;
    private string _expirationDate = string.Empty;
    private string _pin = string.Empty;
    private decimal _creditLimit;
    private decimal _balance;
    private decimal _targetAmount;

    public event CardEventHandler? OnDeposit;
    public event CardEventHandler? OnWithdraw;
    public event CardEventHandler? OnCreditStarted;
    public event CardEventHandler? OnTargetLimitReached;
    public event CardEventHandler? OnPinChanged;

    public string CardNumber
    {
        get
        {
            return _cardNumber;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid card number");
            }
            _cardNumber = value;
        }
    }

    public string OwnerName
    {
        get
        {
            return _ownerName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid owner name");
            }
            _ownerName = value;
        }
    }

    public string ExpirationDate
    {
        get
        {
            return _expirationDate;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid expiration date");
            }
            _expirationDate = value;
        }
    }

    public string Pin
    {
        get
        {
            return _pin;
        }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 4)
            {
                throw new ArgumentException("PIN must contain 4 characters");
            }
            _pin = value;
        }
    }

    public decimal CreditLimit
    {
        get
        {
            return _creditLimit;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Credit limit cannot be negative");
            }
            _creditLimit = value;
        }
    }

    public decimal Balance
    {
        get
        {
            return _balance;
        }
        private set
        {
            _balance = value;
        }
    }

    public decimal TargetAmount
    {
        get
        {
            return _targetAmount;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Target amount cannot be negative");
            }
            _targetAmount = value;
        }
    }

    public CreditCard(
        string cardNumber,
        string ownerName,
        string expirationDate,
        string pin,
        decimal creditLimit,
        decimal balance,
        decimal targetAmount = 0)
    {
        CardNumber = cardNumber;
        OwnerName = ownerName;
        ExpirationDate = expirationDate;
        Pin = pin;
        CreditLimit = creditLimit;
        Balance = balance;
        TargetAmount = targetAmount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Deposit amount must be greater than zero");
        }

        Balance += amount;
        OnDeposit?.Invoke($"Рахунок поповнено на {amount} грн. Поточний баланс: {Balance} грн.");

        if (TargetAmount > 0 && Balance >= TargetAmount)
        {
            OnTargetLimitReached?.Invoke($"Досягнуто заданого ліміту суми {TargetAmount} грн! Поточний баланс: {Balance} грн.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Withdraw amount must be greater than zero");
        }

        if (Balance - amount < -CreditLimit)
        {
            Console.WriteLine("Недостатньо коштів (перевищено кредитний ліміт).");
            return;
        }

        bool wasPositiveOrZero = Balance >= 0;
        Balance -= amount;

        OnWithdraw?.Invoke($"З рахунку витрачено {amount} грн. Поточний баланс: {Balance} грн.");

        if (wasPositiveOrZero && Balance < 0)
        {
            OnCreditStarted?.Invoke($"Розпочато використання кредитних коштів. Кредитний борг: {-Balance} грн.");
        }
    }

    public void ChangePin(string oldPin, string newPin)
    {
        if (oldPin != _pin)
        {
            Console.WriteLine("Невірний старий PIN.");
            return;
        }

        Pin = newPin;
        OnPinChanged?.Invoke("PIN-код успішно змінено.");
    }

    public override string ToString()
    {
        return $"Картка: {CardNumber}, Власник: {OwnerName}, Термін: {ExpirationDate}, Баланс: {Balance} грн, Кредитний ліміт: {CreditLimit} грн";
    }
}