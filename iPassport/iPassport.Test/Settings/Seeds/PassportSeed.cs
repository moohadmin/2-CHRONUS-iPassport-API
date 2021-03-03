using iPassport.Domain.Entities;

namespace iPassport.Test.Seeds
{
    public static class PassportSeed
    {
        public static Passport Get() 
        {
            return new Passport(new UserDetails());
            
        }
           
    }
}
