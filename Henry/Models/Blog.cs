namespace Henry.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Img { get; set; }

        public int CreatorUserId { get; set; }

        public DateTime Created {  get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
