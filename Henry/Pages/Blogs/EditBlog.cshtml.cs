using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class EditBlogModel : PageModel
    {
        private IBlogRepository _blogRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private Blog _blogToUpdate;
        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public Blog BlogToUpdate { get { return _blogToUpdate; } set { _blogToUpdate = value; } }

        public EditBlogModel(IBlogRepository blogRepository, IWebHostEnvironment webHostEnvironment)
        {
            _blogRepo = blogRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet(int id)
        {
            BlogToUpdate = _blogRepo.GetBlog(id);
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (_blogToUpdate.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/EventImages", _blogToUpdate.Img);
                    System.IO.File.Delete(filePath);
                }

                _blogToUpdate.Img = FileHelpers.ProcessUploadedFile("Imgs/blogImages", Photo, _webHostEnvironment);
            }
            else
            {
                _blogToUpdate.Img = _blogRepo.GetBlog(_blogToUpdate.Id).Img;
            }
            BlogToUpdate.LastUpdated = DateTime.Now;
            _blogRepo.UpdateBlog(BlogToUpdate);
            return RedirectToPage("Index");
        }
    }
}
