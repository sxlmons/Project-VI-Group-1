using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceBackend;

[ApiController]
[Route("api/[controller]/[action]")]
public class Test2Controller : ControllerBase
{

    [HttpGet]
    public IActionResult TestApi2()
    {
        return Ok("Response from Test 2");
    }

}