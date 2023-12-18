using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Blogs
{
    public class EditBlogModel : PageModel
    {
        private IBlogRepository _blogRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private IMemberRepository _memberRepository;
        private Blog _blogToUpdate;
        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public Blog BlogToUpdate { get { return _blogToUpdate; } set { _blogToUpdate = value; } }

        public EditBlogModel(IBlogRepository blogRepository, IWebHostEnvironment webHostEnvironment, IMemberRepository memberRepository)
        {
            _blogRepo = blogRepository;
            _webHostEnvironment = webHostEnvironment;
            _memberRepository = memberRepository;
        }

        public void OnGet(int id)
        {
            BlogToUpdate = _blogRepo.GetBlog(id);
        }
        public IActionResult OnPost()
        {
            // verify the user's session is legit first
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (BlogToUpdate.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/EventImages", _blogToUpdate.Img);
                    System.IO.File.Delete(filePath);
                }

                BlogToUpdate.Img = FileHelpers.ProcessUploadedFile("Imgs/blogImages", Photo, _webHostEnvironment);
            }
            else
            {
                BlogToUpdate.Img = null;
            }
            BlogToUpdate.LastUpdated = DateTime.Now;
            _blogRepo.UpdateBlog(BlogToUpdate);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostSamePicture()
        {
            // verify the user's session is legit first
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
                // sets the img as the current img
            BlogToUpdate.Img = _blogRepo.GetBlog(BlogToUpdate.Id).Img;
            BlogToUpdate.LastUpdated = DateTime.Now;
            _blogRepo.UpdateBlog(BlogToUpdate);
            return RedirectToPage("Index");
            
        }
    }
}
