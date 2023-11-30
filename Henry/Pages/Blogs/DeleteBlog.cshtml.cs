using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class DeleteBlogModel : PageModel
    {
        private IBlogRepository _blogRepo;
        private Blog _deleteBlog;
        public Blog DeleteBlog { get { return _deleteBlog; } set { _deleteBlog = value; } }

        public DeleteBlogModel(IBlogRepository blogRepository)
        {
            _blogRepo = blogRepository;
        }

        public IActionResult OnGet(int deleteId)
        {
            DeleteBlog = _blogRepo.GetBlog(deleteId);
            return Page();
        }
        public IActionResult OnPostDelete(int number)
        {
            DeleteBlog = _blogRepo.GetBlog(number);
            _blogRepo.DeleteBlog(DeleteBlog);
            return RedirectToPage("Index");
        }
    }
}
