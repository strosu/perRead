namespace PerRead.Backend.Models.Commands
{
    public class ArticleCommand
    {
        public string Title { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
