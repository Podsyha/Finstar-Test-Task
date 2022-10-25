namespace Postter.Common.Exceptions;

[Serializable]
public sealed class RequestLogicException : Exception
{
    public RequestLogicException() { }

    public RequestLogicException(string message)
        : base(message) { }

    public RequestLogicException(string message, Exception inner)
        : base(message, inner) { }
}