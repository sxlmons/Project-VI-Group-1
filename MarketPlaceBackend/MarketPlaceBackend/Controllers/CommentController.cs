using MarketPlaceBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceBackend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly Logger _logger;
    
    CommentController(ApplicationDbContext db, Logger logger)
    {
        _db = db;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult TestGet()
    {
        return Ok($"Recieved Test Get Request");
    }
    
}