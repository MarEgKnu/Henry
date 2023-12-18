using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class DeleteBlogModel : PageModel
    {
        private IMemberRepository _memberRepo;
        private IBlogRepository _blogRepo;
        private Blog _deleteBlog;
        public Blog DeleteBlog { get { return _deleteBlog; } set { _deleteBlog = value; } }

        public DeleteBlogModel(IBlogRepository blogRepository, IMemberRepository memberRepo)
        {
            _blogRepo = blogRepository;
            _memberRepo = memberRepo;
        }

        public IActionResult OnGet(int id)
        {
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            DeleteBlog = _blogRepo.GetBlog(id);
            return Page();
        }
        public IActionResult OnPostDelete(int number)
        {
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            DeleteBlog = _blogRepo.GetBlog(number);
            _blogRepo.DeleteBlog(DeleteBlog);
            return RedirectToPage("Index");
        }
    }
}
