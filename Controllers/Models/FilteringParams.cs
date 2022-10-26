namespace FINSTAR_Test_Task.Controllers.Models;

public sealed class FilteringParams
{
    public string OnlyValue { get; set; }
    public int? OnlyCode { get; set; }
    public int? MinCode { get; set; }
    public int? MaxCode { get; set; }
    public bool? CodeOrderByDesc { get; set; }
    public bool? CodeOrderByAsc { get; set; }
    public int? CountTakeFirst { get; set; }
}