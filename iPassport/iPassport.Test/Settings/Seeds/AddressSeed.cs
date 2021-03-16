using iPassport.Domain.Entities;
using System;

namespace iPassport.Test.Seeds
{
    public static class AddressSeed
    {
        public static Address Get() => new Address("rua A", Guid.NewGuid(), "43700123", "123", "Centro");

    }
}
