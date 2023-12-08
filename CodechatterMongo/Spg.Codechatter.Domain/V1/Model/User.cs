namespace Spg.Codechatter.Domain.V1.Model
{
    public class User
    {
        public int Id { get; init; }
        public Guid Guid { get; init; } = Guid.NewGuid();
        public string EmailAddress { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid ChatroomId { get; init; }

        private List<Message> _messages = new();
        public virtual IReadOnlyList<Message> Messages => _messages;

        protected User() { }

        public User(Guid chatroomId, string emailAddress, string username, string password)
        {
            ChatroomId = chatroomId;
            EmailAddress = emailAddress;
            Username = username;
            Password = password;
        }
    }
}