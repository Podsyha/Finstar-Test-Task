using Microsoft.AspNetCore.Mvc;

namespace FINSTAR_Test_Task.Controllers;

[ApiController]
[Route("[controller]")]
public class DBController : ControllerBase
{
    private readonly ILogger<DBController> _logger;

    public DBController(ILogger<DBController> logger)
    {
        _logger = logger;
    }


    [HttpPost("/save")]
    public async Task<IActionResult> SaveToDb()
    {
        
    }
}