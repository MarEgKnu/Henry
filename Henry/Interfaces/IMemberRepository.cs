using Henry.Models;

namespace Henry.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetAllMembers();

        Member GetMembers(int id);
    }
}
