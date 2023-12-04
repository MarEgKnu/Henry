
using Henry.Models;

namespace Henry.Interfaces
{
    public interface IBoatRepository
    {
        public void AddBoat(Boat boat);

        public List<Boat> GetAllBoats();

        public bool UpdateBoat(Boat boat);

        public bool DeleteBoat(Boat boat);

        public Boat GetBoat(int id);
    }
}
