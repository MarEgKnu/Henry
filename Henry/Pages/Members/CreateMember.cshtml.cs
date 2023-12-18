using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Henry.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private IMemberRepository _memberRepo;
        private Member _newMember;

        [BindProperty]
        public Member NewMember { get { return _newMember; } set { _newMember = value; } }

        [BindProperty]
        [Required]
        public IFormFile Photo { get; set; }

        public CreateMemberModel(IMemberRepository memberRepository, IWebHostEnvironment environment)
        {
            _memberRepo = memberRepository;
            _environment = environment; 
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost() //Bruges til at oprette/update/delete
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (NewMember.Pb != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "/Imgs/MemberImages", NewMember.Pb);
                    System.IO.File.Delete(filePath);
                }

                NewMember.Pb = FileHelpers.ProcessUploadedFile("Imgs/MemberImages", Photo, _environment);

            }
            
            _memberRepo.CreateMember(NewMember);
            return RedirectToPage("/Index");
        }
    }
}
