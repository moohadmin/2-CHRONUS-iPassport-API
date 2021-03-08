using System.ComponentModel;

namespace iPassport.Domain.Enums
{
    public enum EBreedTypes
    {
        [Description("Yellow")]
        Yellow = 1,
        [Description("White")]
        White,
        [Description("Indigenous")]
        Indigenous,
        [Description("Brown")]
        Brown,
        [Description("Black")]
        Black,
        [Description("NotDeclared")]
        NotDeclared,
    }
}
