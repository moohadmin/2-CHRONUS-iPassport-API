namespace iPassport.Application.Interfaces
{
    public interface IAuth2FactService
    {
        /// <summary>
        /// Send Pin to client
        /// </summary>
        void SendPin();
        
        /// <summary>
        /// Query Sent PIN by idMessage
        /// </summary>
        /// <param name="idMessage">idMessage sent to client</param>
        void FindPinSent(string idMessage);

        
        

    }
}
