using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceBackend;

[ApiController]
[Route("api/[controller]/[action]")]
public class PostController : ControllerBase
{
    [HttpGet]
    public IActionResult TestGet()
    {
        return Ok($"Recieved Test Get Request");
    }
    
}