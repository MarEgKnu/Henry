using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;

namespace Henry.Services
{
    public class MemberRepository:IMemberRepository
    {
        private string JsonFileName = @"Data\JsonMember.json";
        public void CreateMember(Member me)
        {
            List<Member> members = GetAllMembers();
            bool addId;
            for (int i = 1; i <= members.Count + 1; i++)
            {
                addId = true;
                foreach (var item in members)
                {
                    if (i == item.UserId)
                    {
                        addId = false;
                    }
                }
                if (addId)
                {
                    me.UserId = i;
                    break;
                }
            }
            members.Add(me);
            JsonFileWriter<Member>.WriteToJson(members, JsonFileName);
        }
        public List<Member> GetAllMembers()
        {
            return JsonFileReader<Member>.ReadJson(JsonFileName);
        }

        public void DeleteMember(Member me)
        {
            List<Member> members = GetAllMembers();
            foreach (Member m in members)
            {
                if (m.UserId == me.UserId)
                {
                    members.Remove(m);
                    JsonFileWriter<Member>.WriteToJson(members, JsonFileName);
                    break;

                }
            }
   

        }

        public Member GetMember(int id)
        {
            List<Member> members = JsonFileReader<Member>.ReadJson(JsonFileName);
            foreach (var item in members)
            {
                if (item.UserId == id)
                    return item;
            }
            return null;
        }
        public void UpdateMember(Member me)
        {
            if (me != null)
            {
                List<Member> members = GetAllMembers();
                foreach (Member m in members)
                {
                    if (me.UserId == m.UserId)
                    {
                        m.Name = me.Name;
                        m.Phone = me.Phone;
                        m.Email = me.Email;
                        m.Pb = me.Pb;
                        break;
                    }
                }
                JsonFileWriter<Member>.WriteToJson(members, JsonFileName);
            }
        }
    }
}
