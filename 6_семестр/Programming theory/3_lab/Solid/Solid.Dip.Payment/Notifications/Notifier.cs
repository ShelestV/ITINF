namespace Solid.Dip.Notifications;

public class Notifier : INotifable
{
    public void Notify(Message message)
    {
        // Send message
        Thread.Sleep(500);

        Console.WriteLine($"Notified {message}");
    }
}
