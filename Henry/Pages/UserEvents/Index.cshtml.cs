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
        public List<int> IsChecked { get; set; }
        public UserEvent UserEvent { get; set; }
        public Member Member { get; set; }

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
        public IActionResult OnPost()
        {
            //UserEvents.Joined
            //UserEvent.EventId = MemberRepository.GetLoggedInMember(HttpContext).UserId;
            //if (!(MemberRepository.GetLoggedInMember(HttpContext).UserId == UserEvent.UserId))
            //{

            //}
            //Indlæs alle Userevents
            List <UserEvent> userEvents = _userEventRepo.GetAllUserEvents();
            int currentUserId = MemberRepository.GetLoggedInMember(HttpContext).UserId;
            foreach (var isChecked in IsChecked)
            {
                //checke om man er tilmeldt ved at løbe userevents igennem
                UserEvent newUserEvent = new UserEvent(currentUserId, isChecked);
                _userEventRepo.AddUserEvent(newUserEvent);
            }
            return RedirectToPage("/Index");

            //UserEvent.EventId = Is
            //_userEventRepo.AddUserEvent(UserEvent);
        }
    }
}
