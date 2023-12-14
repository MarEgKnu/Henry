using Henry.Models;

namespace Henry.Interfaces
{
    public interface IRepairRepository
    {
        public void AddRepair(Repair repair);

        public List<Repair> GetAllRepairs();

        public bool UpdateRepair(Repair repair);

        public bool DeleteRepair(Repair repair);

        public Repair GetRepair(int id);

        public List<Repair> GetRepairsForBoat(int id);

        public bool HasAnyRepairs(int id);
    }
}
