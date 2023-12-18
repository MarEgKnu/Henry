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
        private IMemberRepository _memberRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private IFormFile _photo;
        private Blog _newBlog;

        [BindProperty]
        public Blog NewBlog { get { return _newBlog; } set { _newBlog = value; } }
        [BindProperty]
        public IFormFile? Photo { get { return _photo; } set { _photo = value; } }

        public CreateBlogModel(IBlogRepository blogRepository, IWebHostEnvironment webHost, IMemberRepository memberRepository)
        {
            this._webHostEnvironment = webHost;
            _blogRepo = blogRepository;
            _memberRepo = memberRepository;
            
        }
        public IActionResult OnGet()
        {
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (NewBlog.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/BlogImages", NewBlog.Img);
                    System.IO.File.Delete(filePath);
                }
                NewBlog.Img = Helpers.FileHelpers.ProcessUploadedFile("Imgs/BlogImages", Photo, _webHostEnvironment);
            }
            NewBlog.Created = DateTime.Now;
            NewBlog.LastUpdated = NewBlog.Created;
            NewBlog.CreatorUserId = (int)HttpContext.Session.GetInt32("UserId");
            _blogRepo.AddBlog(NewBlog);
            return RedirectToPage("Index");
        }
    }
}
