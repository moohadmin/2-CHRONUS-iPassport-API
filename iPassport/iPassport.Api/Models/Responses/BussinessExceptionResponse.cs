namespace iPassport.Api.Models.Responses
{
    /// <summary>
    /// Bussiness Exception Response
    /// </summary>
    public class BussinessExceptionResponse
    {
        /// <summary>
        /// Sucess
        /// </summary>
        public bool Success { get; set; }

        /// Error property
        public string Error { get; private set; }      

        /// Constructor default
        public BussinessExceptionResponse(string error) 
        {
            this.Error = error;
            this.Success = false;
        }
        
    }
}
