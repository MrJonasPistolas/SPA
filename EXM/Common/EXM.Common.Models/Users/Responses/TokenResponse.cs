namespace EXM.Common.Models.Users.Responses
{
    public class TokenResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<UserRoleModel> Roles { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserImageURL { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
