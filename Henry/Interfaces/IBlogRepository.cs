using Henry.Models;

namespace Henry.Interfaces
{
    public interface IBlogRepository
    {
        public void AddBlog(Blog blog);

        public List<Blog> GetAllBlogs();

        public bool UpdateBlog(Blog blog);

        public bool DeleteBlog(Blog blog);

        public Blog GetBlog(int id);
    }
}
