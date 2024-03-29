﻿namespace Solid.Dip.Notifications;

internal class EmailMessage
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"From: {this.From}. To: {this.To}\nSubject: {this.Subject}\n{this.Body}";
    }
}
