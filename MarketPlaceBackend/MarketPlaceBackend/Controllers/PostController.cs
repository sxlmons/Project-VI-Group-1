using Microsoft.AspNetCore.Mvc;
using MarketPlaceBackend.Data;
using MarketPlaceBackend.Models;

namespace MarketPlaceBackend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly Logger _logger;

    public PostController(ApplicationDbContext db, Logger logger)
    {
        _db = db;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult TestGet()
    {
        
        var post = new Posts
        {
            UserId = 1,
            Title = "LISTING",
            Description = "INFO ABOUT LISTING",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Posts.Add(post);

        _db.SaveChanges();
        
        _logger.LogEvent($"userid: {post.UserId} added new post");
        
        return Ok("Added a test post to the database");
    }
    
    [HttpGet]
    public IActionResult TestGetPullingData()
    {
        // pulling the first post with a userId of 1 
        var post = _db.Posts.Where(p => p.UserId == 1).ToList();

        if (post == null)
            return NotFound();
        
        return Ok(post);
    }
}