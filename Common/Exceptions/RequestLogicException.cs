namespace FINSTAR_Test_Task.Common.Exceptions;

[Serializable]
public sealed class RequestLogicException : Exception
{
    public RequestLogicException() { }

    public RequestLogicException(string message)
        : base(message) { }

    public RequestLogicException(string message, Exception inner)
        : base(message, inner) { }
}