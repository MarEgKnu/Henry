using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Boats
{
    public class ReportRepairModel : PageModel
    {

        private IBoatRepository _boatRepository;
        private IMemberRepository _memberRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IRepairRepository _repairRepository;
        [BindProperty]
        public Repair Repair { get; set; }

        public Boat BoatToRepair { get; set; }

        public IFormFile? Photo { get; set; }

        public ReportRepairModel(IMemberRepository memberRepository, IBoatRepository boatRepository, IWebHostEnvironment webHostEnvironment, IRepairRepository repairRepository)
        {
            _memberRepository = memberRepository;
            _boatRepository = boatRepository;
            _webHostEnvironment = webHostEnvironment;
            _repairRepository = repairRepository;
        }
        public IActionResult OnGet(int id)
        {
            // redirect to login page if the user is not verified to be logged in currently
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/Login/LogIn");
            }
            else
            {
                BoatToRepair = _boatRepository.GetBoat(id);
                return Page();
            }
        }
        public IActionResult OnPost(int id)
        {
            // redirect to login page if the user is not verified to be logged in currently
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/Login/LogIn");
            }
            BoatToRepair = _boatRepository.GetBoat(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (Repair.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/RepairImgs", Repair.Img);
                    System.IO.File.Delete(filePath);
                }

                Repair.Img = FileHelpers.ProcessUploadedFile("Imgs/RepairImgs", Photo, _webHostEnvironment);
            }
            Repair.BoatId = id;
            Repair.UserId = (int)HttpContext.Session.GetInt32("UserId");
            Repair.Created = DateTime.Now;
            _repairRepository.AddRepair(Repair);
            return RedirectToPage("Index");
        }
    }
}
