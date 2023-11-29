using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using System.Runtime.CompilerServices;

namespace Henry.Services
{
    public class EventRepository : IEventRepository
    {
        private string _jsonFileName = @"Data\JsonEvent.json";

        public void CreateEvent(Event ev)
        {
            List<Event> events = GetEvents();
            bool addId;
            for (int i = 1; i <= events.Count + 1; i++) 
            {
                addId = true;
                foreach (var item in events)
                {
                    if (i == item.Id)
                    {
                        addId = false;
                    }
                }
                if (addId)
                {
                    ev.Id = i;
                    break;
                }
            }
            events.Add(ev);
            JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
        }

        public void DeleteEvent(Event ev)
        {
            List<Event> events = GetEvents();
            events.Remove(ev);
            JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
        }

        public Event GetEvent(int id)
        {
            foreach (Event evt in GetEvents())
            {
                if (evt.Id == id)
                    return evt;
            }
            return new Event();
        }

        public List<Event> GetEvents()
        {
            return JsonFileReader<Event>.ReadJson(_jsonFileName);
        }

        public void UpdateEvent(Event ev)
        {
            if (ev != null)
            {
                List<Event> events = GetEvents();
                foreach (Event e in events)
                {
                    if (ev.Id == e.Id)
                    {
                        e.Name = ev.Name;
                        e.Description = ev.Description;
                        e.DateTime = ev.DateTime;
                        break;
                    }
                }
                JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
            }
        }
    }
}
