using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class UserVaccineSeed
    {
        public static PagedData<UserVaccine> GetPagedUserVaccines()
        {
            var vac = new List<UserVaccine>()
            {
                new UserVaccine(DateTime.Now, 1, Guid.NewGuid(), Guid.NewGuid()),
                new UserVaccine(DateTime.Now, 2, Guid.NewGuid(), Guid.NewGuid()),
                new UserVaccine(DateTime.Now, 3, Guid.NewGuid(), Guid.NewGuid()),
            };

            return new PagedData<UserVaccine>() { Data = vac };
        }
    }
}
