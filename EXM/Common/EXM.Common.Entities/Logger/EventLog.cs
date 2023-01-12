using EXM.Common.Data.Contracts;

namespace EXM.Common.Entities.Logger
{
    public partial class EventLog : IEntity<int>
    {
        public int Id { get; set; }
        public bool SystemLog { get; set; }
        public int? EventId { get; set; }
        public int? LogLevelId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string StackTrace { get; set; }
        public int? AccountId { get; set; }
    }
}
