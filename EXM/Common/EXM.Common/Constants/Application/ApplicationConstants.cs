namespace EXM.Common.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class CsvHeaders
        {
            public const string Name = "Name";
            public const string Amount = "Amount";
            public const string Date = "Date";
            public const string Description = "Description";
        }
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
            public const string SendUpdateDashboard = "UpdateDashboardAsync";
            public const string ReceiveUpdateDashboard = "UpdateDashboard";
            public const string SendRegenerateTokens = "RegenerateTokensAsync";
            public const string ReceiveRegenerateTokens = "RegenerateTokens";
            public const string ReceiveChatNotification = "ReceiveChatNotification";
            public const string SendChatNotification = "ChatNotificationAsync";
            public const string ReceiveMessage = "ReceiveMessage";
            public const string SendMessage = "SendMessageAsync";
            public const string DarkMode = "DarkModeAsync";

            public const string OnConnect = "OnConnectAsync";
            public const string ConnectUser = "ConnectUser";
            public const string OnDisconnect = "OnDisconnectAsync";
            public const string DisconnectUser = "DisconnectUser";
            public const string OnChangeRolePermissions = "OnChangeRolePermissions";
            public const string LogoutUsersByRole = "LogoutUsersByRole";

            #region Dashboard

            #region Incomes
            public const string SendUpdateIncomesDashboard = "UpdateIncomesDashboardAsync";
            public const string ReceiveUpdateIncomesDashboard = "UpdateIncomesDashboard";
            #endregion
            #region Incomes Categories
            public const string SendUpdateIncomesCategoriesDashboard = "UpdateIncomesCategoriesDashboardAsync";
            public const string ReceiveUpdateIncomesCategoriesDashboard = "UpdateIncomesCategoriesDashboard";
            #endregion
            #region Expenses
            public const string SendUpdateExpensesDashboard = "UpdateExpensesDashboardAsync";
            public const string ReceiveUpdateExpensesDashboard = "UpdateExpensesDashboard";
            #endregion
            #region Expenses Categories
            public const string SendUpdateExpensesCategoriesDashboard = "UpdateExpensesCategoriesDashboardAsync";
            public const string ReceiveUpdateExpensesCategoriesDashboard = "UpdateExpensesCategoriesDashboard";
            #endregion

            #endregion
        }
        public static class Cache
        {
            public const string GetAllIncomeCategoriesCacheKey = "all-income-categories";
            public const string GetAllExpenseCategoriesCacheKey = "all-expense-categories";

            public static string GetAllEntityExtendedAttributesCacheKey(string entityFullName)
            {
                return $"all-{entityFullName}-extended-attributes";
            }

            public static string GetAllEntityExtendedAttributesByEntityIdCacheKey<TEntityId>(string entityFullName, TEntityId entityId)
            {
                return $"all-{entityFullName}-extended-attributes-{entityId}";
            }
        }
        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}
