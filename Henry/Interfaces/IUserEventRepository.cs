using Henry.Models;

namespace Henry.Interfaces
{
    public interface IUserEventRepository
    {
        public void AddUserEvent(UserEvent userEvent);

        public List<UserEvent> GetAllUserEvents();

        public bool DeleteUserEvent(UserEvent userEvent);

        public UserEvent GetUserEvent(int id);
    }
}
