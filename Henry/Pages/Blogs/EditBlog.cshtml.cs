using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class EditBlogModel : PageModel
    {
        private IBlogRepository _blogRepo;
        private Blog _blogToUpdate;

        [BindProperty]
        public Blog BlogToUpdate { get { return _blogToUpdate; } set { _blogToUpdate = value; } }

        public EditBlogModel(IBlogRepository blogRepository)
        {
            _blogRepo = blogRepository;
        }

        public void OnGet(int id)
        {
            BlogToUpdate = _blogRepo.GetBlog(id);
        }
        public IActionResult OnPostUpdate()
        {
            BlogToUpdate.LastUpdated = DateTime.Now;
            _blogRepo.UpdateBlog(BlogToUpdate);
            return RedirectToPage("Index");
        }
    }
}
