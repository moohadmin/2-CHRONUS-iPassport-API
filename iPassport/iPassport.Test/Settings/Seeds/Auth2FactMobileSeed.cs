using iPassport.Domain.Entities;
using System;

namespace iPassport.Test.Seeds
{
    public static class Auth2FactMobileSeed
    {
        public static Auth2FactMobile GetAuth2FactMobile()
        {
            return new Auth2FactMobile(Guid.NewGuid(), "test", "1111", true, "132546879");
        }
    }
}
