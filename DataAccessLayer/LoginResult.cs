

namespace DataAccessLayer
{
    public class LoginResult
    {
        public string Role { get; set; }
        public bool Success { get; set; }
        public int ID { get; set; }
        public string Username { get; set; }

        private static LoginResult instance;

        private LoginResult()
        {
        }

        public static LoginResult GetInstance() => instance ??= new();
    }
}
