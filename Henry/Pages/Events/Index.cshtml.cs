using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventRepository _eventRepo;
        public List<Event> Events { get; private set; }

        public IndexModel(IEventRepository eventRepository)
        {
            _eventRepo = eventRepository;
        }

        public void OnGet()
        {
            Events = _eventRepo.GetEvents();
        }
    }
}
