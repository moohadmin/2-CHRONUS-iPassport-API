using iPassport.Domain.Entities;
using System;

namespace iPassport.Test.Seeds
{
    public static class AddressSeed
    {
        public static Address Get() => new Address("rua A; bairro B", Guid.NewGuid(), "43700123");

    }
}
