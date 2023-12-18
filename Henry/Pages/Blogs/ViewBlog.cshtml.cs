using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Henry.Models;
using Henry.Interfaces;
namespace Henry.Pages.Blogs
{
    public class ViewBlogModel : PageModel
    {
        private IBlogRepository _blogRepository;
        public Blog Blog { get; set; }

        public ViewBlogModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public void OnGet(int id)
        {
            Blog = _blogRepository.GetBlog(id);
        }
    }
}
