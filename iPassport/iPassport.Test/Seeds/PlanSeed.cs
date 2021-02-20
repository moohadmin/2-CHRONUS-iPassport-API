using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class PlanSeed
    {
        public static IList<Plan> GetPlans() =>
            new List<Plan>()
            {
                new Plan("free", "free-plan-test"),
                new Plan("premium", "premium-plan-test-3", 4.25m),
                new Plan("corporate", "corporate-plan-test", 2.0m)
            };
    }
}
