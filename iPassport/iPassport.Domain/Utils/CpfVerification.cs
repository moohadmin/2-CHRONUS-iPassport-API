namespace iPassport.Domain.Utils
{
    public static class CpfVerification
    {
        /// <summary>
        /// Verifica se CPF é válido
        /// </summary>
        /// <param name="cpf">CPF</param>
        /// <returns>Se é válido</returns>
        public static bool Validar(string cpf)
        {
            return ehCpf(cpf);
        }

        private static bool ehCpf(string cpf)
        {
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
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
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }
    }
}
