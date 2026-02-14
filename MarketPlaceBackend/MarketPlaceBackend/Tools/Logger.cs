using MarketPlaceBackend.Data;
using MarketPlaceBackend.Models;

namespace MarketPlaceBackend;

public class Logger : ILogger
{ 
    private readonly ApplicationDbContext _db;

    public Logger(ApplicationDbContext db) { _db = db; }
    
    public void LogEvent(string log)
    {
        _db.Logs.Add(new Logs
            {
                Action = log,
                TimeStamp = DateTime.UtcNow
            }
        );

        _db.SaveChanges();
    }
}
