using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.Events
{
    public class EditEventModel : PageModel
    {
        private IEventRepository _eventRepo;
        private Event _eventToUpdate;

        [BindProperty]
        public IFormFile? Photo { get; set; }
        [BindProperty]
        public Event EventToUpdate { get { return _eventToUpdate; }  set { _eventToUpdate = value; } }

        private IWebHostEnvironment _webHostEnvironment;

        public EditEventModel(IEventRepository eventRepository, IWebHostEnvironment webHostEnvironment)
        {
            _eventRepo = eventRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            EventToUpdate = _eventRepo.GetEvent(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (_eventToUpdate.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/EventImages", _eventToUpdate.Img);
                    System.IO.File.Delete(filePath);
                }

                _eventToUpdate.Img = FileHelpers.ProcessUploadedFile("Imgs/eventImages", Photo, _webHostEnvironment);
            } else
            {
                _eventToUpdate.Img = _eventRepo.GetEvent(_eventToUpdate.Id).Img;
            }
            _eventRepo.UpdateEvent(EventToUpdate);
            return RedirectToPage("Index");
        }
    }
}
