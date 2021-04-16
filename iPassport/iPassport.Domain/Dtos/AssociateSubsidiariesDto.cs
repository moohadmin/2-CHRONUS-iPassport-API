using System;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos
{
    public class AssociateSubsidiariesDto
    {
        public Guid ParentId { get; set; }
        public bool AssociateAll { get; set; }
        public IEnumerable<Guid> Subsidiaries { get; set; }
    }
}
