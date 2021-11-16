using System;
namespace LXP2CYD.Users.Dto
{
    public class UserConfirmAccountDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long ToUserId { get; set; }
        public string Code { get; set; }
        public string BaseUrl { get; set; }

    }
}
