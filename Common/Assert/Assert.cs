using Postter.Common.Exceptions;

namespace FINSTAR_Test_Task.Common.Assert;

/// <summary>
/// Класс-утилита с компонентами для проверки условий
/// </summary>
public class Assert : IAssert
{
    /// <summary>
    /// NullReferenceException если объект null
    /// </summary>
    /// <param name="value">Объект</param>
    /// <exception cref="NullReferenceException"></exception>
    public void IsNull<T>(T value)
    {
        if (value == null)
            throw new NullReferenceException("Object is null");
    }
    
    /// <summary>
    /// NullReferenceException если объект null
    /// </summary>
    /// <param name="value">Объект</param>
    /// <param name="message">Текст ошибки</param>
    /// <exception cref="NullReferenceException"></exception>
    public void IsNull<T>(T value, string message)
    {
        if (value == null)
            throw new NullReferenceException(message);
    }

    /// <summary>
    /// InvalidOperationException если в коллекции нет объектов
    /// </summary>
    /// <param name="collection">Коллекция</param>
    /// <typeparam name="T">Тип коллекции</typeparam>
    public void EmptyCollection<T>(IEnumerable<T> collection)
    {
        if (!collection.Any())
            throw new InvalidOperationException("Collection is empty");
    }

    /// <summary>
    /// RequestLogicException если true
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfTrue(bool boolean, string errorMessage)
    {
        if (boolean)
            throw new RequestLogicException(errorMessage);
    }
    
    /// <summary>
    /// RequestLogicException если false
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfFalse(bool boolean, string errorMessage)
    {
        if (!boolean)
            throw new RequestLogicException(errorMessage);
    }
}