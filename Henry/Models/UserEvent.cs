using Henry.Interfaces;
using Henry.Services;

namespace Henry.Models
{
    public class UserEvent
    {
        public int UserEventId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public UserEvent()
        {
            
        }

        public UserEvent(int userID, int eventId)
        {
            UserId = userID;
            EventId = eventId;
        }
    }
}
