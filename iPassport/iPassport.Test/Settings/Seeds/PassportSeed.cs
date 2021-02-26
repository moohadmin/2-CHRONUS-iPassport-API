using iPassport.Domain.Entities;

namespace iPassport.Test.Seeds
{
    public static class PassportSeed
    {
        public static Passport Get() =>
           new Passport(new UserDetails());
    }
}
