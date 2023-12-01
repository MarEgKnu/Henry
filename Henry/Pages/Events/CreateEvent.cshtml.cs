using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Events
{
    public class CreateEventModel : PageModel
    {
        private IEventRepository _eventRepo;
        private IWebHostEnvironment _webHostEnvironment;
        private IFormFile _photo;
        private Event _newEvent;

        [BindProperty]
        public Event NewEvent { get { return _newEvent; } set { _newEvent = value; } }
        [BindProperty]
        public IFormFile Photo { get { return _photo; } set { _photo = value; } }

        public CreateEventModel(IEventRepository eventRepository, IWebHostEnvironment webHost)
        {
            _eventRepo = eventRepository;
            _webHostEnvironment = webHost;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (NewEvent.Img != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Imgs/EventImages", NewEvent.Img);
                    System.IO.File.Delete(filePath);
                }
                NewEvent.Img = Helpers.FileHelpers.ProcessUploadedFile("Imgs/EventImages", Photo, _webHostEnvironment);
            }
            _eventRepo.CreateEvent(NewEvent);
            return RedirectToPage("Index");
        }
    }
}
