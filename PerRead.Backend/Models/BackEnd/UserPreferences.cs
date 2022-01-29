namespace PerRead.Backend.Models.BackEnd
{
    public class UserPreferences
    {
        public string UserId { get; set; } // Should be a foreign key to the Users table, as well as a primary hey here

        public string ProfileImageUri { get; set; }

        public string UserName { get; set; } // Should be stored here as well, to avoid another query to the users table?;
    }
}
