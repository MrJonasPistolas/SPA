namespace EXM.Base.Responses.Identity
{
    public class TokenResponse
    {
        public string Email { get; set; }
        public List<UserRoleModel> Roles { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserImageURL { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
