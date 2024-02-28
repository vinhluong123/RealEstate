namespace WebAdmin.Models
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool Enabled { get; set; }

        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
