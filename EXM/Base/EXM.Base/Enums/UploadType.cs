using System.ComponentModel;

namespace EXM.Base.Enums
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
