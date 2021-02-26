namespace iPassport.Domain.Utils
{
    public static class CpfUtils
    {
        /// <summary>
        /// verify if is valid CPF 
        /// </summary>
        /// <param name="cpf">CPF</param>
        /// <returns>if valid</returns>
        public static bool Valid(string cpf)
        {
            return IsCpf(cpf);
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11
                || cpf.Contains("00000000000")
                || cpf.Contains("11111111111")
                || cpf.Contains("22222222222")
                || cpf.Contains("33333333333")
                || cpf.Contains("44444444444")
                || cpf.Contains("55555555555")
                || cpf.Contains("66666666666")
                || cpf.Contains("77777777777")
                || cpf.Contains("88888888888")
                || cpf.Contains("99999999999"))
                return false;
            var tempCpf = cpf.Substring(0, 9);
            var sum = 0;

            for (var i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            var rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            var digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest;
            return cpf.EndsWith(digit);
        }
    }
}
