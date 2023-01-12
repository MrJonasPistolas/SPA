using EXM.Common.Models.Users.Requests;
using EXM.Common.Models.Users.Responses;
using EXM.Common.Data.Contracts;
using EXM.Common.Data.Wrapper;

namespace EXM.Services.Contracts
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);
        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}