using ModelLibrary;

namespace DataAccessLayer
{
    public class Session
    {
        public User CurrentUser { get; set; }
        public int SessionId { get; set; }
        public DateTime StartedAt { get; set; }
        public TimeSpan TimeOut { get; set; } = TimeSpan.FromHours(1);
        public bool IsLoggedIn => CurrentUser != null;
        public bool IsExpired => StartedAt.Add(TimeOut) < DateTime.Now;
        public bool IsValid => IsLoggedIn && !IsExpired;

        static Session instance;

        public event EventHandler OnCreated;
        public event EventHandler OnInvalidated;

        private Session()
        {
        }

        public static Session GetInstance()
        {
            if (instance == null)
            {
                Session session = new Session();
            }     
            return instance;
        }

        public void Invalidate()
        {
            CurrentUser = null;
            SessionId = 0;
            StartedAt = DateTime.MinValue;
            OnInvalidated?.Invoke(this, EventArgs.Empty);
        }

        Random random = new();
        public void Create(User user)
        {
            CurrentUser = user;
            SessionId = random.Next(1, int.MaxValue);
            StartedAt = DateTime.Now;
            OnCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
