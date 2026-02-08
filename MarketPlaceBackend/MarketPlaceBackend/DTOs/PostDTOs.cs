namespace MarketPlaceBackend.DTOs
{
    public class PostDTOs
    {

    }
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class UpdatedPostDTO
    {
        public string Title { set; get; }
        public string Description { get; set; }
    }
    public class LandingPagePostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
