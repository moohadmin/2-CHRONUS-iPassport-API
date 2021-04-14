using System.Text.RegularExpressions;

namespace iPassport.Domain.Utils
{
    public static class PhoneNumberUtils
    {
        
        public static bool ValidMobile(string mobileNumber) => IsValidMobilePhoneNumber(mobileNumber);
        
        public static bool ValidLandline(string landNumber) => IsValidLandlineNumber(landNumber);

        #region Private
        private static bool IsValidMobilePhoneNumber(string mobileNumber)
        {
            if (!CommonPhoneNumberValid(mobileNumber))
                return false;
            if (mobileNumber.StartsWith("55")
                && (mobileNumber.Length != 13 || !mobileNumber.Substring(4, 1).Equals("9") || mobileNumber.Substring(2, 2).Equals("00")))
                return false;

            return true;
        }
        private static bool IsValidLandlineNumber(string landNumber)
        {
            if (!CommonPhoneNumberValid(landNumber))
                return false;
            if (landNumber.StartsWith("55") && (landNumber.Length != 12 || landNumber.Substring(2, 2).Equals("00")))
                return false;

            return true;
        }
        private static bool CommonPhoneNumberValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;
            if (!Regex.IsMatch(number, "^[0-9]+$"))
                return false;
            return true;
        } 
        #endregion
    }
}
