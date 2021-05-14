using iPassport.Domain.Entities;
using System;

namespace iPassport.Test.Seeds
{
    public static class AddressSeed
    {
        public static Address Get() => new Address("rua A", Guid.NewGuid(), "43700123", "123", "Centro", "teste");
        public static Address GetLoaded() 
        {
            var address = new Address("rua A", Guid.NewGuid(), "43700123", "123", "Centro", "teste");
            address.City = CitySeed.GetLoaded();
            return address;
        }
        

    }
}
