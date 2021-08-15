namespace User.API.Model
{
    public class AccountInfoRequest : BaseRequest
    {
        public string NewName { get; set; }

        public string NewEmail { get; set; }
            
        public int NewRoleId { get; set; }
            
        public byte[] NewPic { get; set; }
    }
}
