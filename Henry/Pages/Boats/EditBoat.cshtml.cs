using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepository _boatRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private IMemberRepository _memberRepo;

        [Required(ErrorMessage = "Billede påkrævet")]
        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public Boat Boat { get; set; }

        [Required(ErrorMessage = "Denne knap er påkrævet")]
        [BindProperty(SupportsGet = true)]
        public bool? ChangePhoto {  get; set; }
        public EditBoatModel(IBoatRepository boatRepo, IWebHostEnvironment webHostEnvironment, IMemberRepository memberRepo)
        {
            _boatRepo = boatRepo;
            _webHostEnvironment = webHostEnvironment;
            _memberRepo = memberRepo;
        }
        public IActionResult OnGet(int id)
        {
            // checks the user is a verified administrator upon entering
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            // Gets the boat, but if the radio button is clicked, it also runs OnGet, and overwrites over input back to defaults? how to fix this
            // maybe get rid of the buttons to decide whether to edit photo or not, and just dont overwrite the photo is none is selected?
            Boat = _boatRepo.GetBoat(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            //if (Photo == null && ChangePhoto == true)
            //{
            //    ModelState.AddModelError("ChangePhoto", "Photo expected as ChangePhoto is true");
            //}

            // checks every potential ModelState for errors, and singles out the lack of photo error to be skipped, if ChangePhoto is false
            foreach (var modStateEntry in ModelState)
            {
                // checks if the individal instance field input is valid
                if (modStateEntry.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    // if it is not valid, check if the input value is null, if the user wants to change photo, and if the variable in question is Photo
                    // basically to proceed even if there photo is null, cause the user dosent want to change it
                    if (modStateEntry.Value.AttemptedValue == null && ChangePhoto == false && modStateEntry.Key == "Photo")
                    {
                        // gets the original boats Img path and pastes it to the model boat instance, to keep the same img as before
                        Boat.Img = _boatRepo.GetBoat(Boat.Id).Img;
                        continue;  
                    }
                    else
                    {
                        return Page();
                    }
                }
            }
            if (Photo != null)
            {
                if (Boat.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/BoatImgs", Boat.Img);
                    System.IO.File.Delete(filePath);
                }

                Boat.Img = FileHelpers.ProcessUploadedFile("Imgs/BoatImgs", Photo, _webHostEnvironment);

            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _boatRepo.UpdateBoat(Boat);
            return RedirectToPage("Index");
        }
        public IActionResult OnGetReg()
        {
            return Page();
        }
    }
}
