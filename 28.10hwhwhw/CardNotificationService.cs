using System;

namespace HomeworkEventsAndExtensions;

internal class CardNotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"[Сповіщення]: {message}");
    }
}