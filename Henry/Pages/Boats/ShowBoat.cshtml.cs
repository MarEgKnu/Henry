using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Boats
{
    public class ShowBoatModel : PageModel
    {


        private IBoatRepository _boatRepository;

        public Boat BoatToView { get; set; }
        public ShowBoatModel(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }
        public void OnGet(int id)
        {
            BoatToView = _boatRepository.GetBoat(id);
        }
    }
}
