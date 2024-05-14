using System.Data;

namespace ModelLibrary
{
    public class User
    {
        private static int increment = 0;
        public int UserID { get; init; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public User()
        {
            UserID = increment++;
        }

        public User(string userName, string password)
        {
            UserID = increment++;

            UserName = userName;

            Password = password;

            Role = Role.User;
        }
    }
    public enum Role
    {
        User = 1,
        Admin = 2,
        None = 4
    }
}