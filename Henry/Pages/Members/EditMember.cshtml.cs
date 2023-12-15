using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Henry.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;

namespace Henry.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepository _repo;
        private IWebHostEnvironment _environment;
        [BindProperty]
        public IFormFile? Photo {  get; set; }

        [BindProperty]
        public Member MemberToUpdate { get; set; }

        public EditMemberModel(IMemberRepository memberRepository, IWebHostEnvironment webHostEnvironment)
        {

            _repo = memberRepository;
            _environment = webHostEnvironment;
        }
        public IActionResult OnGet(int id)
        {
            if (!_repo.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            MemberToUpdate = _repo.GetMember(id);
            return Page();
        }

        public IActionResult OnPostUpdate(int id)
        {
            if (!_repo.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Photo != null)
            {
                if (MemberToUpdate.Pb != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "/Imgs/MemberImages", MemberToUpdate.Pb);
                    System.IO.File.Delete(filePath);
                }

                MemberToUpdate.Pb = FileHelpers.ProcessUploadedFile("Imgs/MemberImages", Photo, _environment);

            }
            // if no img is selected, keep current image
            else
            {
                MemberToUpdate.Pb = _repo.GetMember(id).Pb;
            }
            _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("/LogIn/ProfilePage");
        }
      }
}
