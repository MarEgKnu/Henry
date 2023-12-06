using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;

namespace Henry.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberRepository _memberRepo;
        private Member _deleteMember;
        public Member DeleteMember { get { return _deleteMember; } set { _deleteMember = value; } }

        public DeleteMemberModel(IMemberRepository memberRepository)
        {
            _memberRepo = memberRepository;
        }

        public IActionResult OnGet(int deleteUserId)
        {
            DeleteMember = _memberRepo.GetMember(deleteUserId);
            return Page();
        }
        public IActionResult OnPostDelete(int deleteUserId)
        {
            DeleteMember = _memberRepo.GetMember(deleteUserId);
            _memberRepo.DeleteMember(DeleteMember);
            return RedirectToPage("Index");
        }
    } 
}
