using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.LogIn
{
    public class LogInModel : PageModel
    {
        private IMemberRepository _memberRepo;

        [BindProperty]
        [Required(ErrorMessage = "Angiv brugernavn"), DisplayName("Brugernavn")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Angiv kodeord"), DisplayName("Kodeord")]

        public string Password { get; set; }

        public string Message { get; set; }

        public LogInModel(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Member Member = _memberRepo.VerifyUser(Name, Password);
            if (Member == null)
            {
                Message = "Brugernavn eller kodeordet er forkert";
                Name = "";
                Password = "";
                return Page();
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", Member.UserId);
                HttpContext.Session.SetString("Name", Member.Name);
                HttpContext.Session.SetString("Password", Member.Password);
                return RedirectToPage("ProfilePage");
            }
        }
    }
}
