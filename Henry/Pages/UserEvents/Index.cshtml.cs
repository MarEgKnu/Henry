using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.UserEvents
{
    public class IndexModel : PageModel
    {
        private IEventRepository _eventRepo;
        private IUserEventRepository _userEventRepo;
        public IMemberRepository MemberRepository { get; set; }
        public List<Event> Events { get; private set; }
        public List<UserEvent> UserEvents { get; set; }
        [BindProperty]
        public bool? IsChecked { get; set; }
        public UserEvent UserEvent { get; set; }

        public IndexModel(IEventRepository eventRepository, IUserEventRepository userEventRepo, IMemberRepository memberRepository)
        {
            _eventRepo = eventRepository;
            _userEventRepo = userEventRepo;
            MemberRepository = memberRepository;
        }
        public IActionResult OnGet()
        {
            Events = _eventRepo.GetEvents();
            if (!MemberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            else
            {
                return Page();
            }

        }
        public void OnPost()
        {
            //UserEvents.Joined
            UserEvent.EventId = MemberRepository.GetLoggedInMember(HttpContext).UserId;
            _userEventRepo.AddUserEvent(UserEvent);
        }
    }
}
