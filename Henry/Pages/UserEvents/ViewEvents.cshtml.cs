using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.UserEvents
{
    public class ViewEventsModel : PageModel
    {
        private IUserEventRepository _userEventRepo;
        public List<UserEvent> UserEvents { get; set; }
        public ViewEventsModel(IUserEventRepository userEventRepo)
        {
            _userEventRepo = userEventRepo;
        }
        public void OnGet()
        {
            UserEvents = _userEventRepo.GetAllUserEvents();
        }
    }
}
