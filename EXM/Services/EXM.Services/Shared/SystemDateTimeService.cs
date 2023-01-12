using EXM.Common.Data.Contracts;

namespace EXM.Services.Shared
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
