using iPassport.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class Auth2FactService
    {
        private readonly ISmsExternalService _smsExternalServices;
        public Auth2FactService(ISmsExternalService smsExternalServices)
        {
            _smsExternalServices = smsExternalServices;
        }

        public void AuthClient()
        {
            
        }
    }
}
