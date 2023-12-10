using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {

        private IBoatRepository _boatRepo;
        private IMemberRepository _memberRepo;
      

        public Boat BoatToDelete;
        public DeleteBoatModel(IBoatRepository boatRepo, IMemberRepository memberRepo)
        {
            _boatRepo = boatRepo;
            _memberRepo = memberRepo;
        }
        public IActionResult OnGet(int id)
        {
            // checks the user is a verified administrator upon entering
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            BoatToDelete = _boatRepo.GetBoat(id);
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            _boatRepo.DeleteBoat(_boatRepo.GetBoat(id));
            return RedirectToPage("Index");
        }
    }
}
