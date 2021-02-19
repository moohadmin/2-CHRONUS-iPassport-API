namespace iPassport.Application.Interfaces
{
    public interface IAuth2FactService
    {
        /// <summary>
        /// Teste Envio de SMS
        /// </summary>
        void AuthClientSend();
        /// <summary>
        /// Teste Consulta de Envio de SMS
        /// </summary>
        void AuthClientRecieve();

    }
}
