using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Henry.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepository _repo;

        [BindProperty]
        public Member MemberToUpdate { get; set; }

        public EditMemberModel(IMemberRepository memberRepository)
        {

            _repo = memberRepository;

        }
        public void OnGet(int id)
        {
            MemberToUpdate = _repo.GetMember(id);
        }

        public IActionResult OnPostUpdate()
        {
            _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("Index");
        }
      }
}
