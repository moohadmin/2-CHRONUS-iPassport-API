using iPassport.Domain.Enums;
using System;

namespace iPassport.Application.Models.ViewModels
{
    public class UserDiseaseTestViewModel
    {
        public Guid Id { get; set; }
        public Guid DiseaseId { get; set; }
        public Guid UserId { get; set; }
        public bool? Result { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime? ResultDate { get; set; }
        public EDiseaseTestStatus Status { get; set; }
    }
}
