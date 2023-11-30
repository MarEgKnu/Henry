using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public enum BoatType
    {
        Jolle,
        Sejlskib,
        motorbåd
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
        public DateTime? Created {  get; set; }
        [Display(Name = "Har brug for reperationer")]
        [Required(ErrorMessage = "Denne knap er krævet")]
        public bool? NeedsRepair { get; set; }  // to be implemented later
        [Display(Name = "Bådtype")]
        [Required(ErrorMessage = "Bådtype er krævet")]
        public BoatType? Type { get; set; }

        public override string ToString()
        {
            return $"Navn: {Name}\n" +
                   $"ID: {Id.ToString()}\n" +
                   $"Beskrivelse: {Description}\n" +
                   $"Oprettet: {Created}\n" +
                   $"Har brug for reparationer?: {NeedsRepair}\n" +
                   $"Bådtype: {Type}";
        }
    }
}
