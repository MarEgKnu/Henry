using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class CreateBlogModel : PageModel
    {
        private IBlogRepository _blogRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private IFormFile _photo;
        private Blog _newBlog;

        [BindProperty]
        public Blog NewBlog { get { return _newBlog; } set { _newBlog = value; } }
        [BindProperty]
        public IFormFile Photo { get { return _photo; } set { _photo = value; } }

        public CreateBlogModel(IBlogRepository blogRepository, IWebHostEnvironment webHost)
        {
            this._webHostEnvironment = webHost;
            _blogRepo = blogRepository;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (Photo != null)
            {
                if (NewBlog.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Images/BlogImages", NewBlog.Img);
                    System.IO.File.Delete(filePath);
                }
                NewBlog.Img = Helpers.FileHelpers.ProcessUploadedFile("/Images/BlogImages", Photo, _webHostEnvironment);
            }
            NewBlog.Created = DateTime.Now;
            NewBlog.LastUpdated = NewBlog.Created;
            _blogRepo.AddBlog(NewBlog);
            return RedirectToPage("Index");
        }
    }
}
