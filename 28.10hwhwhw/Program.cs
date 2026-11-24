using System;

namespace HomeworkEventsAndExtensions;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Завдання 1: Перевірка дужок ===");

        string[] testStrings = { "{}[]", "(())", "[{}]", "[}", "[[{]}]" };

        foreach (string str in testStrings)
        {
            Console.WriteLine($"{str} -> {str.IsValidBrackets()}");
        }

        Console.WriteLine();
        Console.WriteLine("=== Завдання 2: Кредитна картка ===");

        CreditCard card = new CreditCard(
            "4149 4991 2233 4455",
            "Іваненко Іван Іванович",
            "12/28",
            "1234",
            5000m,
            1000m,
            3000m
        );

        CardNotificationService notifier = new CardNotificationService();

        card.OnDeposit += notifier.SendNotification;
        card.OnWithdraw += notifier.SendNotification;
        card.OnCreditStarted += notifier.SendNotification;
        card.OnTargetLimitReached += notifier.SendNotification;
        card.OnPinChanged += notifier.SendNotification;

        Console.WriteLine(card);
        Console.WriteLine();

        card.Deposit(2500m);
        card.Withdraw(4000m);
        card.Withdraw(5000m);
        card.ChangePin("1234", "4321");

        Console.WriteLine();
        Console.WriteLine(card);
    }
}