namespace FINSTAR_Test_Task.Controllers.Models;

/// <summary>
/// DTO модель для общения с БД
/// </summary>
public sealed class CodeValueDto
{
    /// <summary>
    /// Код
    /// </summary>
    public int Code { get; set; }
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; }
}