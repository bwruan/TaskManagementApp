namespace User.Domain.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public byte[] ProfilePic { get; set; }

        public bool Status { get; set; }
    }
}
