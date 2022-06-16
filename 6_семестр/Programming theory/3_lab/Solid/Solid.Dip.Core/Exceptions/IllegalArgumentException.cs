namespace Solid.Dip;

internal class IllegalArgumentException : ArgumentException
{
    public IllegalArgumentException(string? message = null) : base(message)
    {
    }
}
