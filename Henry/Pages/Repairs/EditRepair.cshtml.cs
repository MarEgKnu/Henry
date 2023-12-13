using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Repairs
{
    public class EditRepairModel : PageModel
    {
        private IMemberRepository _memberRepository;
        private IRepairRepository _repairRepository;
        private IWebHostEnvironment _webHostEnvironment;
        public EditRepairModel(IRepairRepository repairRepository, IMemberRepository memberRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repairRepository = repairRepository;
            _memberRepository = memberRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public IFormFile? Photo { get; set; }
        [BindProperty]
        public Repair Repair { get; set; }
        public IActionResult OnGet(int id)
        {
            // verify the user's session is legit first
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            Repair = _repairRepository.GetRepair(id);
            // Only proceed if the user is an administrator OR the user is the creator of the repair
            if (_memberRepository.VerifySessionAdmin(HttpContext) || _repairRepository.GetRepair(id).UserId == HttpContext.Session.GetInt32("UserId"))
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
        }
        public IActionResult OnPost(int id)
        {
            // verify the user's session is legit first
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            Repair = _repairRepository.GetRepair(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Only proceed if the user is an administrator OR the user is the creator of the repair
            if (_memberRepository.VerifySessionAdmin(HttpContext) || _repairRepository.GetRepair(id).UserId == HttpContext.Session.GetInt32("UserId"))
            {
                // img processing
                if (Photo != null)
                {
                    if (Repair.Img != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/RepairImgs", Repair.Img);
                        System.IO.File.Delete(filePath);
                    }

                    Repair.Img = FileHelpers.ProcessUploadedFile("Imgs/RepairImgs", Photo, _webHostEnvironment);

                }
                // if no img is selected, remove the current picture
                else
                {
                    Repair.Img = null;
                }
                _repairRepository.UpdateRepair(Repair);
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
        }
        public IActionResult OnPostSamePicture(int id)
        {
            // verify the user's session is legit first
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            //Repair = _repairRepository.GetRepair(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Only proceed if the user is an administrator OR the user is the creator of the repair
            if (_memberRepository.VerifySessionAdmin(HttpContext) || _repairRepository.GetRepair(id).UserId == HttpContext.Session.GetInt32("UserId"))
            {
                // sets the img as the current img
                Repair.Img = _repairRepository.GetRepair(id).Img;
                _repairRepository.UpdateRepair(Repair);
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
        }
    }
}
