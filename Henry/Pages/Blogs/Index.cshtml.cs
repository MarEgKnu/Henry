using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private IBlogRepository _blogRepo;
        public List<Blog> Blogs { get; private set; }

        public IndexModel(IBlogRepository blogRepository)
        {
            _blogRepo = blogRepository;
        }

        public void OnGet()
        {
            Blogs = _blogRepo.GetAllBlogs();
            // reverse the list so the newest blog entries are shown at the top
            Blogs.Reverse();
        }
    }
}
