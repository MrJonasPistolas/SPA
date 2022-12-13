using EXM.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EXM.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        #region > Properties <
        public string UserId { get; set; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
        #endregion

        #region > Constructor <
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }
        #endregion
    }
}
