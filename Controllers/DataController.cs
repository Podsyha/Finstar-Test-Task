using System.Text.Json;
using FINSTAR_Test_Task.Controllers.Models;
using FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FINSTAR_Test_Task.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    public DataController(ICodeValueRepository codeValueRepository)
    {
        _codeValueRepository = codeValueRepository;
    }
    
    private readonly ICodeValueRepository _codeValueRepository;


    /// <summary>
    /// Сохранить данные
    /// </summary>
    /// <param name="json">Json объект</param>
    /// <returns></returns>
    [HttpPost("/save-data")]
    public async Task<IActionResult> SaveToDb([FromBody] JsonDocument json)
    {
        List<CodeValueDto> content = JsonConvertToObj(json);
        await _codeValueRepository.AddDataCollection(content);
        
        return CreatedAtAction(nameof(GetData), null);
    }

    /// <summary>
    /// Получить данные с возможностью фильтрации
    /// </summary>
    /// <param name="filteringParams">Фильтры</param>
    /// <returns></returns>
    [HttpGet("/data")]
    public async Task<IActionResult> GetData([FromQuery] FilteringParams filteringParams)
    {
        ICollection<CodeValueUi> response = await _codeValueRepository.GetData(filteringParams);

        if (!response.Any())
            return NoContent();

        return Ok(JsonSerializer.Serialize(response));
    }

    
    /// <summary>
    /// Конвертировать json в заданном шаблоне в коллекцию данных
    /// </summary>
    /// <param name="json">Json объект</param>
    /// <returns></returns>
    private static List<CodeValueDto> JsonConvertToObj(JsonDocument json)
    {
        JArray array = JArray.Parse(json.RootElement.ToString());
        List<CodeValueDto> codeValues = new();

        foreach (JObject obj in array.Children<JObject>())
        {
            codeValues.AddRange(obj.Properties().Select(singleProp => new CodeValueDto()
            {
                Code = Convert.ToInt32(singleProp.Name),
                Value = singleProp.Value.ToString()
            }));
        }

        return codeValues;
    }
}