namespace PerRead.Backend.Models.FrontEnd
{
    public class FEUserPreview
    {
        public string UserId { get; set; }

        public string UserName { get; set; } // Should be taken from here, do not include it in the token anymore

        public string ProfileImageUri { get; set; }

        public int ReadingTokens { get; set; }
    }
}
