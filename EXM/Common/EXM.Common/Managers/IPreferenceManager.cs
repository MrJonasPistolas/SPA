using EXM.Common.Settings;
using EXM.Common.Wrapper;

namespace EXM.Common.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}
