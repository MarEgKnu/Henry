namespace Henry.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Img { get; set; }

        public int CreatorUserId { get; set; }

        public DateTime Created {  get; set; }

        public DateTime LastUpdated { get; set; }


        //public override string ToString()
        //{
        //    IMemberRepository memberRepo = new IMenuRepository();
        //    return $"Titel: {Title}\n" +
        //           $"Oprettet af: {memberRepo.GetMember(CreatorUserId)}\n" +
        //           $"Oprettet: {Created}\n" +
        //           $"Sidst opdateret: {LastUpdated}\n" +
        //           $"ID: {Id}";
        //}
    }
}
