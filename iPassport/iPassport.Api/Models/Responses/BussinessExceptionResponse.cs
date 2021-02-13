namespace iPassport.Api.Models.Responses
{
    /// <summary>
    /// Bussiness Exception Response
    /// </summary>
    public class BussinessExceptionResponse
    {
        /// Error property
        public string Error { get; private set; }


        /// Constructor default
        public BussinessExceptionResponse(string error) => this.Error = error;
    }
}
