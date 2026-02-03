using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceBackend;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController : ControllerBase
{
    [HttpGet]
    public IActionResult TestGet()
    {
        return Ok($"Recieved Test Get Request");
    }
    
}