using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.Useful
{
    public class AuthorOwnership
    {
        public Author Author { get; set; }

        public double Ownership { get; set; }

        public bool CanBeEdited { get; set; }

        public bool IsUserFacing { get; set; }
    }
}
