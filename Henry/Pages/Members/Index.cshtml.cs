using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
namespace Henry.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository _repoMember;
        public List<Member> Members { get; private set; }
        //constructor
        public IndexModel(IMemberRepository memberRepository)
        {
            _repoMember = memberRepository;
        }
        public void OnGet()
        {
            Members = _repoMember.GetAllMembers();
        }
    }
}
