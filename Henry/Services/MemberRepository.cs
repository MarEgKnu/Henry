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
            if (userName == null || passWord == null)
            {
                return null;
            }
            foreach (var user in GetAllMembers())
            {
               
                if (userName.Equals(user.Name) && passWord.Equals(user.Password))
                {
                    return user;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns true or false if the current HttpContext is a valid Member ID, Name and Password combination
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns>Bool</returns>
        public bool VerifySession(HttpContext HttpContext)
        {
            if (HttpContext.Session.GetString("Name") == null || HttpContext.Session.GetString("Password") == null || HttpContext.Session.GetInt32("UserId") == null)
            {
                return false;
            }
            foreach (var user in GetAllMembers())
            {
                if (user.Name == HttpContext.Session.GetString("Name") && user.Password == HttpContext.Session.GetString("Password") && user.UserId == HttpContext.Session.GetInt32("UserId"))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Returns true or false if the current HttpContext is a valid Member ID, Name and Password combination and has administator rights
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns>Bool</returns>
        public bool VerifySessionAdmin(HttpContext HttpContext)
        {
            if (HttpContext.Session.GetString("Name") == null || HttpContext.Session.GetString("Password") == null || HttpContext.Session.GetInt32("UserId") == null)
            {
                return false;
            }
            foreach (var user in GetAllMembers())
            {
                // now both checks if the user is valid, and if they are an admin
                if (user.Name == HttpContext.Session.GetString("Name") && user.Password == HttpContext.Session.GetString("Password") && user.UserId == HttpContext.Session.GetInt32("UserId") && user.Memberstatus)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Gets the currently logged in member, or null if not logged in or verification fails
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns>A member object</returns>
        public Member GetLoggedInMember(HttpContext HttpContext)
        {
            // verification
            if (!VerifySession(HttpContext))
            {
                return null;
            }
            Member user = GetMember((int)HttpContext.Session.GetInt32("UserId"));
            return user;
        }
    }
}
