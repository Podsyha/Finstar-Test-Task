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


    [HttpPost("/save-data")]
    public async Task<IActionResult> SaveToDb([FromBody] JsonDocument json)
    {
        try
        {
            List<CodeValueDto> content = JsonConvertToObj(json);
            await _codeValueRepository.AddSorted(content);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return CreatedAtAction(nameof(GetAllData), null);
    }

    [HttpGet("/data")]
    public async Task<IActionResult> GetAllData([FromQuery] FilteringParams filteringParams)
    {
        var response = await _codeValueRepository.GetData(filteringParams);

        return Ok(JsonSerializer.Serialize(response));
    }

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