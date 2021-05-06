using System;

namespace iPassport.Application.Extensions
{
    public static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string strEnumValue)
        {
            if (!Enum.TryParse(typeof(TEnum), strEnumValue, out object enumValue))
                throw new InvalidCastException();

            return (TEnum)enumValue;
        }
    }
}
