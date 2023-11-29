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

        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Boat NewBoat { get; set; }
        [Required(ErrorMessage = "Photo er krævet")]
        [BindProperty]
        public IFormFile Photo { get; set; }

        //public SelectList Types { get; set; }

        public CreateBoatModel(IBoatRepository boatRepo, IWebHostEnvironment webHost)
        {
            _boatRepo = boatRepo;
            webHostEnvironment = webHost;
            //Types = new SelectList(BoatType, "Type", "Type");
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost() // bruges til at oprette/update/delete
        {
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
