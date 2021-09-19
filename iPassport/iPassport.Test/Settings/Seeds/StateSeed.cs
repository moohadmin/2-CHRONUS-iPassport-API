using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class StateSeed
    {
        public static State GetState() => new State("Bahia", "BA", 123, Guid.NewGuid(), 10);


        public static IList<State> GetStates()
        {
            return new List<State>()
            {
                new State("Bahia", "BA", 123, Guid.NewGuid(), 10),
                new State("Rio de Janeiro", "RJ", 1234, Guid.NewGuid(), null),
            };
        }

        public static PagedData<State> GetPaged()
        {
            return new PagedData<State>() { Data = GetStates() };
        }
    }
}
