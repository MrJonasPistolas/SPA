using EXM.Common.Constants.Localization;
using EXM.Common.Settings;

namespace EXM.Application.WebAPI.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}