using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.UserEvents
{
    public class IndexModel : PageModel
    {
        private IEventRepository _eventRepo;
        private IUserEventRepository _userEventRepo;
        public List<Event> Events { get; private set; }
        public List<UserEvent> UserEvents { get; set; }

        public IndexModel(IEventRepository eventRepository, IUserEventRepository userEventRepo)
        {
            _eventRepo = eventRepository;
            _userEventRepo = userEventRepo;
        }
        public void OnGet()
        {
            Events = _eventRepo.GetEvents();
            //UserEvents = _userEventRepo.GetAllUserEvents();
        }
        public void OnPostCheck()
        {
            //foreach (UserEvent userEvent in UserEvents)
            //{
            //    if (checked == true) {
            //        userEvent.Joined = true;
            //    }
            //}
        }
    }
}
