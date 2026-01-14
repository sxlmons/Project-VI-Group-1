using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace MarketPlaceBackend;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult TestGet()
    {
        return Ok($"Recieved Test Get Request");
    }
    
    [HttpPost]
    public IActionResult TestPost(PayloadExample payload)
    {
        return Ok($"Received POST for id={payload.Id}, name={payload.Name}");
    }
    
    [HttpPut]
    public IActionResult TestPut(PayloadExample payload)
    {
        return Ok($"Received PUT for id={payload.Id}, new name={payload.Name}");
    }
    
    [HttpPatch]
    public IActionResult TestPatch(PayloadExample payload)
    {
        return Ok($"Received PATCH for id={payload.Id}, patch new name={payload.Name}");
    }
    
    [HttpDelete]
    public IActionResult TestDelete(int id)
    {
        return Ok($"Received DELETE for id={id}");
    }

    [HttpOptions]
    public IActionResult TestOptions()
    {
        Response.Headers.Allow = "GET,POST,PUT,PATCH,DELETE,OPTIONS";
        return Ok("Allowed: GET, POST, PUT, PATCH, DELETE, OPTIONS");
    }
}

// SYS-040 says all the payloads have to be JSON 
// This JSON property makes organizing that easy 

public class PayloadExample
{
    // The JsonPropertyName is what is labelled in the front end 
    [JsonPropertyName("id")]
    // and this variable is the backends
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}