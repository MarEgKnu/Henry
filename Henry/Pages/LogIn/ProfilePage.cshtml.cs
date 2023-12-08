using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.LogIn
{
    public class ProfilePageModel : PageModel
    {
        public Member CurrentMember { get; set; }
        private IMemberRepository _memberRepository;

        public ProfilePageModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") == null) 
            {
                RedirectToPage("LogInError");
            }
            // Consider casting a VerifyUser here to veryfy again, and if it fails, log out
            CurrentMember = _memberRepository.GetMember((int)HttpContext.Session.GetInt32("UserId"));
            return Page();
        }
    }
}
