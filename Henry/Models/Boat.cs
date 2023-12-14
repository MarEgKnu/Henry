using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public enum BoatType
    {
        Jolle,
        Sejlskib,
        Motorbåd
    }
    public class Boat
    {
        public int Id { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn er krævet")]
        public string? Name { get; set; }
        [Display(Name = "Beskrivelse")]
        [Required(ErrorMessage = "Beskrivelse er krævet")]
        public string? Description { get; set; }

        public string? Img { get; set; }
        [Display(Name = "Oprettet")]
        public DateTime Created {  get; set; }
        [Display(Name = "Bådtype")]
        [Required(ErrorMessage = "Bådtype er krævet")]
        public BoatType? Type { get; set; }

        public override string ToString()
        {
            return $"Navn: {Name}\n" +
                   $"ID: {Id.ToString()}\n" +
                   $"Beskrivelse: {Description}\n" +
                   $"Oprettet: {Created}\n" +
                   $"Bådtype: {Type}";
        }
    }
}
