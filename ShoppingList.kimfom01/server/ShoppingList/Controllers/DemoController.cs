using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult GetHello()
    {
        _logger.LogInformation("Demo get endpoint called");

        return Ok(new { message = "Hello from demo controller" });
    }
}
