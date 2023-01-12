using System.ComponentModel;

namespace EXM.Common.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document,

        [Description(@"Incomes")]
        Incomes,

        [Description(@"Expenses")]
        Expenses
    }
}
