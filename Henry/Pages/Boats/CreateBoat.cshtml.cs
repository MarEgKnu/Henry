using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
        private IBoatRepository _boatRepo;
        private IMemberRepository _memberRepo;
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Boat NewBoat { get; set; }
        [Required(ErrorMessage = "Photo er krævet")]
        [BindProperty]
        public IFormFile Photo { get; set; }

        //public SelectList Types { get; set; }

        public CreateBoatModel(IBoatRepository boatRepo, IWebHostEnvironment webHost, IMemberRepository memberRepo)
        {
            _boatRepo = boatRepo;
            webHostEnvironment = webHost;
            _memberRepo = memberRepo;
            //Types = new SelectList(BoatType, "Type", "Type");
        }
        public IActionResult OnGet()
        {
            // checks the user is a verified administrator upon entering
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            return Page();
        }
        public IActionResult OnPost() // bruges til at oprette/update/delete
        {
            // checks the user is a verified administrator upon posting
            if (!_memberRepo.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (NewBoat.Img != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Imgs/BoatImgs", NewBoat.Img);
                    System.IO.File.Delete(filePath);
                }

                NewBoat.Img = FileHelpers.ProcessUploadedFile("Imgs/BoatImgs", Photo, webHostEnvironment);
            }
            NewBoat.Created = DateTime.Now;
            _boatRepo.AddBoat(NewBoat);
            return RedirectToPage("Index");

        }
        /// <summary>
        /// Processes the uploaded file by generating a unique name and copying it to the BoatImgs folder
        /// </summary>
        /// <returns>File name as a string</returns>
        //private string ProcessUploadedFile(string path)
        //{
        //    string uniqueFileName = null;
        //    if (Photo != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Photo.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}
