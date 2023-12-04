using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;

namespace Henry.Services
{
    public class BlogRepository : IBlogRepository
    {
        private string jsonFileName = @"Data\BlogData.json";
        /// <summary>
        /// Adds the given blog param to the repository, and gives it a new unique ID
        /// </summary>
        /// <param name="blog"></param>
        public void AddBlog(Blog blog)
        {
            List<int> BlogIds = new List<int>();

            List<Blog> blogs = GetAllBlogs();

            foreach (var aBlog in blogs)
            {
                BlogIds.Add(aBlog.Id);
            }
            if (BlogIds.Count != 0)
            {
                int start = BlogIds.Max();
                blog.Id = start + 1;
            }
            else
            {
                blog.Id = 1;
            }
            blogs.Add(blog);
            JsonFileWriter<Blog>.WriteToJson(blogs, jsonFileName);
        }
        /// <summary>
        /// Deletes a blog with the same ID as the provided param from the repository
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>bool</returns>
        public bool DeleteBlog(Blog blog)
        {
            bool sucess;
            // checks if it deleted anything, if not return false
            List<Blog> blogs = GetAllBlogs();
            foreach (Blog b in blogs)
            {
                if (b.Id == blog.Id)
                {
                    sucess = blogs.Remove(b);
                    // deletes the img associated with the blog, if it was able to delete it and img isnt null
                    if (b.Img != null && sucess)
                    {
                        string[] paths = { _env.WebRootPath, "Imgs", "BlogImages", b.Img };
                        string path = Path.Combine(paths);
                        // if the file exists delete it
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                    }
                    JsonFileWriter<Blog>.WriteToJson(blogs, jsonFileName);
                    return sucess;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets all blogs currently in the repository
        /// </summary>
        /// <returns>A list of blogs</returns>
        public List<Blog> GetAllBlogs()
        {
            return JsonFileReader<Blog>.ReadJson(jsonFileName);
        }
        /// <summary>
        /// Returns the blog with an id matching the param. A empty blog object is returned if none is found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A blog object if found, or an empty blod obj if none found</returns>
        public Blog GetBlog(int id)
        {
            foreach (var blog in GetAllBlogs())
            {
                if (blog.Id == id)
                {
                    return blog;
                }
            }
            return new Blog();
        }
        /// <summary>
        /// Updates the blog with the same ID as the given param and overwrites all relevant attributes
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>True if sucessfull, false if not</returns>

        public bool UpdateBlog(Blog blog)
        {
            if (blog != null)
            {
                List<Blog> blogs = GetAllBlogs();
                foreach (var blo in blogs)
                {
                    if (blo.Id == blog.Id)
                    {
                        blo.Title = blog.Title;
                        blo.Img = blog.Img;
                        blo.Content = blog.Content;
                        blo.LastUpdated = DateTime.Now;
                        JsonFileWriter<Blog>.WriteToJson(blogs, jsonFileName);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
