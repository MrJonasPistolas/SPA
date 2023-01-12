using EXM.Common.Entities.Logger;

namespace EXM.Data.Logger.Contracts
{
    public interface IEventLogRepository
    {
        bool Add(EventLog log);
        IQueryable<EventLog> Get();
    }
}
