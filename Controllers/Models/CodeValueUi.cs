namespace FINSTAR_Test_Task.Controllers.Models;

/// <summary>
/// Модель выдачи результата данных в UI
/// </summary>
public class CodeValueUi
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