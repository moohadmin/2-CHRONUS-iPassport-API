using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class CompanySegmentDto
    {
        public CompanySegmentDto() { }
        public CompanySegmentDto(CompanySegment segment)
        {
            Id = segment.Id;
            Name = segment.Name;            
            Identifyer = segment.Identifyer;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Identifyer { get; set; }
    }
}