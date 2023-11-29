using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;

namespace Henry.Services
{
    public class MemberRepository:IMemberRepository
    {
        private string JsonFileName = @"Data\JsonMember.json";

        public List<Member> GetAllMembers()
        {
            return JsonFileReader<Member>.ReadJson(JsonFileName);
        }

 
        
        public Member GetMembers(int id)
        {
            List<Member> members = JsonFileReader<Member>.ReadJson(JsonFileName);
            foreach (var item in members)
            {
                if (item.UserId == id)
                    return item;
            }
            return null;
        }

    }
}
