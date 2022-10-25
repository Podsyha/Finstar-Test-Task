using System.Text.Json;
using FINSTAR_Test_Task.Controllers.Models;
using FINSTAR_Test_Task.Infrastructure.Repository.CodeValueRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FINSTAR_Test_Task.Controllers;

[ApiController]
[Route("[controller]")]
public class DBController : ControllerBase
{
    public DBController(ILogger<DBController> logger, ICodeValueRepository codeValueRepository)
    {
        _logger = logger;
        _codeValueRepository = codeValueRepository;
    }

    private readonly ILogger<DBController> _logger;
    private readonly ICodeValueRepository _codeValueRepository;


    [HttpPost("/save")]
    public async Task<IActionResult> SaveToDb(string json)
    {
        try
        {
            List<CodeValueDto> content = JsonConvertToObj(json);
            await _codeValueRepository.AddSorted(content);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
        
        //Дописать location
        return Ok();
    }

    private static List<CodeValueDto> JsonConvertToObj(string json)
    {
        JArray array = JArray.Parse(json);
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