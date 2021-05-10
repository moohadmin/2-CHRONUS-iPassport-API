using System;
using System.Globalization;
using System.Linq;
using System.Text;

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

        public static string RemoveDiacritics(this string str)
        {
            if (null == str) return null;
            var chars = str
                .Normalize(NormalizationForm.FormD)
                .ToCharArray()
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
