using Henry.Models;

namespace Henry.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetAllMembers();

        Member GetMember(int id);
        public void CreateMember(Member member);

        public void UpdateMember(Member member);

        public void DeleteMember(Member member);

    }
}
