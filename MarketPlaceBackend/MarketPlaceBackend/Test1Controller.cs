using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceBackend;

[ApiController]
[Route("api/[controller]/[action]")]
public class Test1Controller : ControllerBase
{

    [HttpGet]
    public IActionResult TestApi1()
    {
        return Ok("Response from Test 1");
    }

}