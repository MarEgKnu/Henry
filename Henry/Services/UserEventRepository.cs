using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Identity;

namespace Henry.Services
{
    public class UserEventRepository : IUserEventRepository
    {
        private string _jsonFileName = @"Data\JsonUserEvent.json";
        public void AddUserEvent(UserEvent userEvent)
        {
            List<UserEvent> userEvents = GetAllUserEvents();
            bool addId;
            for (int i = 1; i <= userEvents.Count + 1; i++)
            {
                addId = true;
                foreach (var item in userEvents)
                {
                    if (i == item.UserEventId)
                    {
                        addId = false;
                    }
                }
                if (addId)
                {
                    userEvent.UserEventId = i;
                    break;
                }
            }
            userEvents.Add(userEvent);
            JsonFileWriter<UserEvent>.WriteToJson(userEvents, _jsonFileName);
        }

        public bool DeleteUserEvent(UserEvent userEvent)
        {
            bool sucess;
            List<UserEvent> userEvents = GetAllUserEvents();
            foreach (UserEvent ue in userEvents)
            {
                if (ue.UserEventId == userEvent.UserEventId)
                {
                    sucess = userEvents.Remove(ue);
                    JsonFileWriter<UserEvent>.WriteToJson(userEvents, _jsonFileName);
                    return sucess;
                }
            }
            return false;
        }

        public List<UserEvent> GetAllUserEvents()
        {
            return JsonFileReader<UserEvent>.ReadJson(_jsonFileName);
        }

        public UserEvent GetUserEvent(int id)
        {
            foreach (UserEvent userEvent in GetAllUserEvents())
            {
                if (userEvent.UserEventId == id)
                {
                    return userEvent;
                }
            }
            return new UserEvent();
        }
    }
}
