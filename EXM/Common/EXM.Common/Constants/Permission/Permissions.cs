using System.Reflection;

namespace EXM.Common.Constants.Permission
{
    public static class Permissions
    {
        public static class Loadings
        {
            public const string IncomesView = "Permissions.Loadings.IncomesView";
            public const string IncomesLoad = "Permissions.Loadings.IncomesLoad";
            public const string ExpensesView = "Permissions.Loadings.ExpensesView";
            public const string ExpensesLoad = "Permissions.Loadings.ExpensesLoad";
        }
        public static class Incomes
        {
            public const string View = "Permissions.Incomes.View";
            public const string Create = "Permissions.Incomes.Create";
            public const string Edit = "Permissions.Incomes.Edit";
            public const string Delete = "Permissions.Incomes.Delete";
            public const string Export = "Permissions.Incomes.Export";
            public const string Search = "Permissions.Incomes.Search";
        }

        public static class IncomeCategories
        {
            public const string View = "Permissions.IncomeCategories.View";
            public const string Create = "Permissions.IncomeCategories.Create";
            public const string Edit = "Permissions.IncomeCategories.Edit";
            public const string Delete = "Permissions.IncomeCategories.Delete";
            public const string Export = "Permissions.IncomeCategories.Export";
            public const string Search = "Permissions.IncomeCategories.Search";
        }

        public static class Expenses
        {
            public const string View = "Permissions.Expenses.View";
            public const string Create = "Permissions.Expenses.Create";
            public const string Edit = "Permissions.Expenses.Edit";
            public const string Delete = "Permissions.Expenses.Delete";
            public const string Export = "Permissions.Expenses.Export";
            public const string Search = "Permissions.Expenses.Search";
        }

        public static class ExpenseCategories
        {
            public const string View = "Permissions.ExpenseCategories.View";
            public const string Create = "Permissions.ExpenseCategories.Create";
            public const string Edit = "Permissions.ExpenseCategories.Edit";
            public const string Delete = "Permissions.ExpenseCategories.Delete";
            public const string Export = "Permissions.ExpenseCategories.Export";
            public const string Search = "Permissions.ExpenseCategories.Search";
        }

        public static class Documents
        {
            public const string View = "Permissions.Documents.View";
            public const string Create = "Permissions.Documents.Create";
            public const string Edit = "Permissions.Documents.Edit";
            public const string Delete = "Permissions.Documents.Delete";
            public const string Search = "Permissions.Documents.Search";
        }

        public static class DocumentTypes
        {
            public const string View = "Permissions.DocumentTypes.View";
            public const string Create = "Permissions.DocumentTypes.Create";
            public const string Edit = "Permissions.DocumentTypes.Edit";
            public const string Delete = "Permissions.DocumentTypes.Delete";
            public const string Export = "Permissions.DocumentTypes.Export";
            public const string Search = "Permissions.DocumentTypes.Search";
        }

        public static class DocumentExtendedAttributes
        {
            public const string View = "Permissions.DocumentExtendedAttributes.View";
            public const string Create = "Permissions.DocumentExtendedAttributes.Create";
            public const string Edit = "Permissions.DocumentExtendedAttributes.Edit";
            public const string Delete = "Permissions.DocumentExtendedAttributes.Delete";
            public const string Export = "Permissions.DocumentExtendedAttributes.Export";
            public const string Search = "Permissions.DocumentExtendedAttributes.Search";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        public static class Communication
        {
            public const string Chat = "Permissions.Communication.Chat";
        }

        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }

        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }

        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }
        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();

            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}
