using EXM.Common.Entities.Logger;
using Microsoft.Data.SqlClient;

namespace EXM.Data.Logger.Repositories
{
    public class EventLogRepository
    {
        private readonly string _connection;
        private static readonly string _addEventLogInsertCmd;

        static EventLogRepository()
        {
            _addEventLogInsertCmd = "insert into [dbo].[aspneteventlog] ([eventid],[systemlog],[loglevelid],[loglevel],[message],[stacktrace],[categoryname],[createdtime],[accountid]) values (@eventid,@systemlog, @loglevelid, @loglevel, @message, @stacktrace, @categoryname, getdate(), @accountid)";
        }

        public EventLogRepository(string connection)
        {
            _connection = connection;
        }

        public bool Add(EventLog log)
        {

            List<SqlParameter> paramList = new List<SqlParameter>
            {
                new SqlParameter("EventID", log.EventId),
                new SqlParameter("LogLevelId", log.LogLevelId),
                new SqlParameter("LogLevel", log.LogLevel),
                new SqlParameter("Message", log.Message),
                new SqlParameter("CategoryName", log.CategoryName),
                new SqlParameter("SystemLog", log.SystemLog)
            };

            if (log.StackTrace != null)
                paramList.Add(new SqlParameter("StackTrace", log.StackTrace));
            else
                paramList.Add(new SqlParameter("StackTrace", DBNull.Value));

            if (log.AccountId != null)
                paramList.Add(new SqlParameter("AccountId", log.AccountId));
            else
                paramList.Add(new SqlParameter("AccountId", DBNull.Value));

            return ExecuteNonQuery(_addEventLogInsertCmd, paramList);
        }

        private bool ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    int count = command.ExecuteNonQuery();
                    conn.Close();
                    result = count > 0;
                }
            }
            return result;
        }

    }
}
