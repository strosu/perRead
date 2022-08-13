using System;
namespace PerRead.Backend.Models.Commands
{
    public class UpdateOwnershipCommand
    {
        public IEnumerable<OwnerDescription> Owners { get; set; }
    }

    public class OwnerDescription
    {
        public string AuthorId { get; set; }

        public double OwnershipPercent { get; set; }
    }
}

