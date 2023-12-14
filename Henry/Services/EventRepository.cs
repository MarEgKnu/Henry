using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Henry.Services
{
    public class EventRepository : IEventRepository
    {
        private string _jsonFileName = @"Data\JsonEvent.json";
        private IWebHostEnvironment _env;

        public EventRepository(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

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
            ev.Joined = false;
            events.Add(ev);
            JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
        }

        public void DeleteEvent(Event ev)
        {
            bool sucess;
            List<Event> events = GetEvents();
            foreach (Event evt in events)
            {
                if (evt.Id == ev.Id)
                {
                    sucess = events.Remove(evt);
                    if (evt.Img != null && sucess)
                    {
                        string[] paths = { _env.WebRootPath, "Imgs", "EventImages", evt.Img };
                        string path = Path.Combine(paths);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                    }
                    JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
                    break;
                }
            }
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
                        if (e.Img != null && e.Img != ev.Img)
                        {
                            string[] paths = { _env.WebRootPath, "Imgs", "EventImages", e.Img };
                            string path = Path.Combine(paths);
                            // if the file exists delete it
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                        }
                        e.Img = ev.Img;
                        break;
                    }
                }
                JsonFileWriter<Event>.WriteToJson(events, _jsonFileName);
            }
        }
    }
}
