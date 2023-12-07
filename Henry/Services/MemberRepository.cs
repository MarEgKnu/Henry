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
        public Member VerifyUser(string userName, string passWord)
        {
            foreach (var user in GetAllMembers())
            {
                if (userName == null || passWord == null)
                {
                    return null;
                }
                else if (userName.Equals(user.Name) && passWord.Equals(user.Password))
                {
                    return user;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the currently logged in member, or null if not logged in or verification fails
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns>A member object</returns>
        public Member GetLoggedInMember(HttpContext HttpContext)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return null;
            }
            Member user = GetMember((int)HttpContext.Session.GetInt32("UserId"));
            if (user == null)
            {
                return null;
            }
            // verification
            if (user.Password == HttpContext.Session.GetString("Password") && HttpContext.Session.GetString("Name") == user.Name)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
