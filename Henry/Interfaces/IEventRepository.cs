using Henry.Models;

namespace Henry.Interfaces
{
    public interface IEventRepository
    {
        void CreateEvent(Event ev);
        List<Event> GetEvents();
        Event GetEvent(int id);
        void UpdateEvent(Event ev);
        void DeleteEvent(Event ev);
    }
}
