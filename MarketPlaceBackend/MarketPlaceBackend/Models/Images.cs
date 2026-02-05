namespace MarketPlaceBackend.Models;

public class Images
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string ImagePath { get; set; }
    public DateTime UploadedAt { get; set; }
}