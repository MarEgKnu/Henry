using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Events
{
    public class EditEventModel : PageModel
    {
        private IEventRepository _eventRepo;
        private Event _eventToUpdate;

        [BindProperty]
        public Event EventToUpdate { get { return _eventToUpdate; }  set { _eventToUpdate = value; } }

        public EditEventModel(IEventRepository eventRepository)
        {
            _eventRepo = eventRepository;
        }

        public void OnGet(int id)
        {
            EventToUpdate = _eventRepo.GetEvent(id);
        }
        public IActionResult OnPostUpdate()
        {
            _eventRepo.UpdateEvent(EventToUpdate);
            return RedirectToPage("Index");
        }
    }
}
