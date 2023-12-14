using Henry.Helpers;
using Henry.Interfaces;
using Henry.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public class BoatBooking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }
        [BindProperty]
        public List<int>? ExtraUsersIds { get; set; }

        public List<string> AllMemberNames
        {
            get
            {
                List<string> result = new List<string>();
                IMemberRepository memberRepo = new MemberRepository();
                if (memberRepo.GetMember(UserId) != null)
                {
                    result.Add(memberRepo.GetMember(UserId).Name);
                }
                if (ExtraUsersIds != null)
                {
                    foreach (int id in ExtraUsersIds)
                    {
                        result.Add(memberRepo.GetMember(id).Name);
                    }
                }
                return result;
            }
        }


        public List<string> ExtraMemberNames
        {
            get
            {
                List<string> result = new List<string>();
                IMemberRepository memberRepo = new MemberRepository();
                if (ExtraUsersIds != null)
                {
                    foreach (int id in ExtraUsersIds)
                    {
                        result.Add(memberRepo.GetMember(id).Name);
                    }
                }
                return result;
            }
        }


        public List<int> AllMemberIds
        {
            get
            {
                List<int> result = new List<int>();
                IMemberRepository memberRepo = new MemberRepository();
                result.Add(UserId);
                if (ExtraUsersIds != null)
                {
                    foreach (int id in ExtraUsersIds)
                    {
                        result.Add(id);
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// returns true if the booking has exceeded its planned end
        /// </summary>
        public bool ExceededTime 
        {
            get
            {
                if (BookingEnd < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public int BoatId { get; set; }

        [Required(ErrorMessage = "Feltet er krævet")]
        [BindProperty]
        [IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]

        public DateTime BookingStart { get; set; }

        [Required(ErrorMessage = "Feltet er krævet")]
        [BindProperty]
        [IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]
        public DateTime BookingEnd { get; set;}

    }
}
