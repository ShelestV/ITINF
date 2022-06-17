namespace Solid.Dip.Notifications;

public class Message
{
    public string Content { get; set; } = string.Empty;

    public override string ToString()
    {
        return this.Content;
    }
}
