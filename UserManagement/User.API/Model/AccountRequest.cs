namespace User.API.Model
{
    public class AccountRequest : BaseRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string ProfilePic { get; set; }
    }
}
