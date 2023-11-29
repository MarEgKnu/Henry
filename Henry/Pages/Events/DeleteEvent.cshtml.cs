using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Events
{
    public class DeleteEventModel : PageModel
    {
        private IEventRepository _eventRepo;
        private Event _deleteEvent;
        public Event DeleteEvent { get { return _deleteEvent; } set { _deleteEvent = value; } }

        public DeleteEventModel(IEventRepository eventRepository)
        {
            _eventRepo = eventRepository;
        }

        public IActionResult OnGet(int deleteId)
        {
            DeleteEvent = _eventRepo.GetEvent(deleteId);
            return Page();
        }
        public IActionResult OnPostDelete(int number)
        {
            DeleteEvent = _eventRepo.GetEvent(number);
            _eventRepo.DeleteEvent(DeleteEvent);
            return RedirectToPage("Index");
        }
    }
}
