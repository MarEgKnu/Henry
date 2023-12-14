using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Repairs
{
    public class DeleteRepairModel : PageModel
    {
        private IMemberRepository _memberRepository;
        private IRepairRepository _repairRepository;
        private IBoatRepository _boatRepository;

        public Repair RepairToDelete { get; set; }

        public DeleteRepairModel(IMemberRepository memberRepository, IRepairRepository repairRepository, IBoatRepository boatRepository)
        {
            _memberRepository = memberRepository;
            _repairRepository = repairRepository;
            _boatRepository = boatRepository;
            
        }
        public IActionResult OnGet(int id)
        {
            // checks the user is a verified administrator upon entering
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            RepairToDelete = _repairRepository.GetRepair(id);
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            // checks the user is a verified administrator upon entering
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            RepairToDelete = _repairRepository.GetRepair(id);
            _repairRepository.DeleteRepair(RepairToDelete);
            return RedirectToPage("Index");
        }
    }
}
