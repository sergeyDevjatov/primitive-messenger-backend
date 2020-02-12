namespace primitive_messenger_backend.Database.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
