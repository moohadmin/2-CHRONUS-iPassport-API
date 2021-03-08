using System.ComponentModel;

namespace iPassport.Domain.Enums
{
    public enum EGendersTypes
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("NotBinary")]
        NotBinary,
        [Description("NotDeclared")]
        NotDeclared,
    }
}
