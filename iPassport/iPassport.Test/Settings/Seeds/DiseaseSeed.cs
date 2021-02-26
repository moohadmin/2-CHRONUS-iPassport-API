using iPassport.Application.Models.Pagination;
using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class DiseaseSeed
    {
        public static PagedResponseApi GetPagedDisease()
        {
            return new PagedResponseApi(true, null, 1, 3, 10, 300, new List<Disease>() { new Disease("Test", "test"), new Disease("Test-2", "test-2"), new Disease("Test-3", "test-3") });
        }
    }
}
