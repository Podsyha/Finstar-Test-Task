namespace FINSTAR_Test_Task.Controllers.Models;

/// <summary>
/// Фильтры на выдачу данных из БД
/// </summary>
public sealed class FilteringParams
{
    /// <summary>
    /// Фильтр значения value
    /// </summary>
    public string OnlyValue { get; set; }
    /// <summary>
    /// Фильтр значения code
    /// </summary>
    public int? OnlyCode { get; set; }
    /// <summary>
    /// Фильтр минимального поля code
    /// </summary>
    public int? MinCode { get; set; }
    /// <summary>
    /// Фильтр максимального поля code
    /// </summary>
    public int? MaxCode { get; set; }
    /// <summary>
    /// Фильтр сортировки по-убыванию
    /// </summary>
    public bool? CodeOrderByDesc { get; set; }
    /// <summary>
    /// Фильтр сортировки по-возрастанию
    /// </summary>
    public bool? CodeOrderByAsc { get; set; }
    /// <summary>
    /// Фильтр получения первых N значений
    /// </summary>
    public int? CountTakeFirst { get; set; }
}