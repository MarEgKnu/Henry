using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Henry.Interfaces;
using Henry.Services;
namespace Henry.Models
{
    public class Repair
    {
        public int  RepairId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Beskrivelse er krævet")]
        public string Description { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Titel er krævet")]
        public string Title { get; set; }

        public DateTime Created {  get; set; }

        public string? Img { get; set; }

        public int UserId { get; set; }

        public int BoatId { get; set; }

        // returns the username of the creator of the repair
        public string? UserName
        {
            get
            {
                IMemberRepository memberRepository = new MemberRepository();
                if (memberRepository.GetMember(UserId) != null)
                {
                    return memberRepository.GetMember(UserId).Name;
                }
                else
                {
                    return null;
                }
            }
        }


        // returns the boat the repair is attached to
        public string? BoatName
        {
            get
            {
                IBoatRepository boatRepository = new BoatRepository();
                if (boatRepository.GetBoat(BoatId) != null)
                {
                    return boatRepository.GetBoat(BoatId).Name;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
