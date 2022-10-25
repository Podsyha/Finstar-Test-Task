namespace FINSTAR_Test_Task.Infrastructure.DAO;

/// <summary>
/// Сущность пользователя
/// </summary>
public class CodeValueEntity
{
    /// <summary>
    /// Порядковый номер
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Код
    /// </summary>
    public int Code { get; set; }
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; }
}