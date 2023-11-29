using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {

        private IBoatRepository _boatRepo;

        public Boat BoatToDelete;
        public DeleteBoatModel(IBoatRepository boatRepo)
        {
            _boatRepo = boatRepo;
        }
        public void OnGet(int id)
        {
            BoatToDelete = _boatRepo.GetBoat(id);
        }
        public IActionResult OnPost(int id)
        {
            _boatRepo.DeleteBoat(_boatRepo.GetBoat(id));
            return RedirectToPage("Index");
        }
    }
}
