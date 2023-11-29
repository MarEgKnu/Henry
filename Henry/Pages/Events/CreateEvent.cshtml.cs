using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Events
{
    public class CreateEventModel : PageModel
    {
        private IEventRepository _eventRepo;
        private Event _newEvent;

        [BindProperty]
        public Event NewEvent { get { return _newEvent; } set { _newEvent = value; } }

        public CreateEventModel(IEventRepository eventRepository)
        {
            _eventRepo = eventRepository;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _eventRepo.CreateEvent(NewEvent);
            return RedirectToPage("Index");
        }
    }
}
