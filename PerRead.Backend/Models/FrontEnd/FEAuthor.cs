namespace PerRead.Backend.Models.FrontEnd
{
    public class FEAuthor 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FESection> Sections { get; set; }

        public string AuthorImageUri { get; set; }
    }
}
