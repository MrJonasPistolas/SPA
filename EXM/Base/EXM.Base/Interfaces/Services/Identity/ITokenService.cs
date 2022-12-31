using EXM.Base.Interfaces.Common;
using EXM.Base.Requests.Identity;
using EXM.Base.Responses.Identity;
using EXM.Common.Wrapper;

namespace EXM.Base.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);
        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}