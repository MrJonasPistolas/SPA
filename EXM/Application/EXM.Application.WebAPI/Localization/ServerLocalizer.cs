using Microsoft.Extensions.Localization;

namespace EXM.Application.WebAPI.Localization
{
    internal class ServerLocalizer<T> where T : class
    {
        public IStringLocalizer<T> Localizer { get; }

        public ServerLocalizer(IStringLocalizer<T> localizer)
        {
            Localizer = localizer;
        }
    }
}
