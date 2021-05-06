using System;

namespace iPassport.Domain.Utils
{
    public static class EnumUtils
    {
        public static TEnum ToEnum<TEnum>(this string strEnumValue)
        {
            if (!Enum.TryParse(typeof(TEnum), strEnumValue, out object enumValue))
                throw new InvalidCastException();
            
            return (TEnum)enumValue;
        }
    }
}
