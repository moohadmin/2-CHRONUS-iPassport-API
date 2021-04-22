using iPassport.Domain.Dtos;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using iPassport.Domain.Utils;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, Guid? planId = null, IList<UserVaccine> userVaccines = null, bool? wasCovidInfected = null, string bond = null, Guid? priorityGroupId = null,
            IList<UserDiseaseTest> userDiseaseTests = null, bool? wasTestPerformed = null) : base()
        {
            Id = userId;

            if (planId.HasValue)
                PlanId = planId.Value;

            if (userVaccines != null)
                UserVaccines = userVaccines;

            if (wasCovidInfected != null)
                WasCovidInfected = wasCovidInfected.Value;

            if (bond != null)
                Bond = bond;

            if (priorityGroupId != null)
                PriorityGroupId = priorityGroupId;

            if (userDiseaseTests != null)
                UserDiseaseTests = userDiseaseTests;

            WasTestPerformed = wasTestPerformed;
        }

        public bool? WasCovidInfected { get; private set; }
        public string Bond { get; private set; }
        /// <summary>
        /// Depreciated must use PriorityGroupId
        /// </summary>
        public string PriorityGroup { get; private set; }

        public Guid? PlanId { get; private set; }
        public Guid? PriorityGroupId { get; private set; }
        public virtual Guid? ImportedFileId { get; set; }
        public bool? WasTestPerformed { get; private set; }
        public Guid? HealthUnitId { get; private set; }

        public virtual Plan Plan { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual PriorityGroup PPriorityGroup { get; set; }

        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }
        public virtual IEnumerable<UserDiseaseTest> UserDiseaseTests { get; set; }

        public virtual ImportedFile ImportedFile { get; set; }
        public virtual HealthUnit HealthUnit { get; set; }


        public UserDetails Create(UserAgentCreateDto dto) =>
            new UserDetails(dto.UserId);

        public UserDetails Create(CitizenCreateDto dto) =>
            new UserDetails(dto.Id, userVaccines: CreateUservaccine(dto.Doses), wasCovidInfected: dto.WasCovidInfected, bond: dto.Bond, priorityGroupId: dto.PriorityGroupId,
                userDiseaseTests: CreateUserDiseaseTest(dto.Test), wasTestPerformed: dto.WasTestPerformed);

        private IList<UserVaccine> CreateUservaccine(IList<UserVaccineCreateDto> dto)
        {
            var uservaccines = new List<UserVaccine>();

            foreach (var d in dto)
                uservaccines.Add(new UserVaccine().Create(d));

            return uservaccines;
        }

        private IList<UserDiseaseTest> CreateUserDiseaseTest(UserDiseaseTestCreateDto dto)
        {
            var userDiseaseTests = new List<UserDiseaseTest>();
            if (dto == null)
                return userDiseaseTests;

            userDiseaseTests.Add(new UserDiseaseTest().Create(dto));

            return userDiseaseTests;
        }

        public void AssociatePlan(Guid plandId) => PlanId = plandId;

        public bool IsApprovedPassport()
        {
            bool isApproved = true;

            if (UserVaccines == null || !UserVaccines.Any(x => x.ExclusionDate == null))
                isApproved = false;

            var vacinnes = UserVaccines.Where(x => x.ExclusionDate == null).Select(x => x.Vaccine).Distinct().ToList();

            if (vacinnes == null || !vacinnes.Any())
            {
                isApproved = false;
            }
            else
            {
                foreach (var vaccine in vacinnes)
                {
                    if (vaccine == null || GetUserVaccineStatus(vaccine.Id) != EUserVaccineStatus.Immunized)
                        isApproved = false;
                }
            }

            if (!isApproved)
            {
                if ((UserDiseaseTests == null || !UserDiseaseTests.Any()) || (GetDiseaseTestStatus() != EDiseaseTestStatus.Negative))
                    isApproved = false;

                else
                    isApproved = true;
            }

            return isApproved;
        }

        public EUserVaccineStatus GetUserVaccineStatus(Guid vaccineId)
        {
            if (UserVaccines == null || !UserVaccines.Any(x => x.VaccineId == vaccineId && x.ExclusionDate == null))
                return EUserVaccineStatus.Unvaccinated;

            var vaccine = UserVaccines.FirstOrDefault(x => x.VaccineId == vaccineId && x.ExclusionDate == null).Vaccine;

            DateTime lastVaccineDate = DateTime.MinValue;
            int validDoses = 0;

            foreach (var userVaccine in UserVaccines.Where(x => x.VaccineId == vaccineId && x.ExclusionDate == null).OrderBy(x => x.Dose))
            {
                if (vaccine.UniqueDose())
                    return userVaccine.IsImmunized() ? EUserVaccineStatus.Immunized : EUserVaccineStatus.Vaccinated;

                else if (!userVaccine.IsFirstDose())
                {
                    if (userVaccine.VaccinationDate >= lastVaccineDate.AddDays(vaccine.MinTimeNextDose)
                        && userVaccine.VaccinationDate <= lastVaccineDate.AddDays(vaccine.MaxTimeNextDose))
                        validDoses += 1;
                    else
                        return EUserVaccineStatus.NotImmunized;
                }
                else
                    validDoses += 1;

                lastVaccineDate = userVaccine.VaccinationDate;
            }

            if (validDoses == vaccine.RequiredDoses)
            {
                var lastDose = UserVaccines.FirstOrDefault(x => x.VaccineId == vaccineId && x.Dose == vaccine.RequiredDoses && x.ExclusionDate == null);

                return lastDose.IsImmunized() ? EUserVaccineStatus.Immunized : EUserVaccineStatus.Vaccinated;
            }
            else
                return EUserVaccineStatus.Waiting;
        }

        public EDiseaseTestStatus GetDiseaseTestStatus(Guid? testId = null)
        {
            var validTests = UserDiseaseTests?.Where(x => (DateTime.UtcNow - x.TestDate).TotalHours <= Constants.DISEASE_TEST_VALIDATE_IN_HOURS
                                                            && (testId == null || x.Id == testId)).ToList();

            if (validTests == null || !validTests.Any())
                return EDiseaseTestStatus.Expired;

            if (validTests.All(x => x.Result == null || x.ResultDate == null))
                return EDiseaseTestStatus.Waiting;

            if (validTests.Any(x => x.Result == true))
                return EDiseaseTestStatus.Positive;

            return EDiseaseTestStatus.Negative;
        }

        public void ChangeCitizen(CitizenEditDto dto)
        {
            Bond = dto.Bond;
            WasCovidInfected = dto.WasCovidInfected;
            WasTestPerformed = dto.WasTestPerformed;
            PriorityGroupId = dto.PriorityGroupId;
        }

        public static UserDetails CreateUserDetail(UserImportDto dto, Guid importedFileId)
            => new()
            {
                Id = dto.UserId,
                Bond = dto.Bond,
                PriorityGroupId = dto.PriorityGroupId,
                WasCovidInfected = dto.WasCovidInfectedBool,
                WasTestPerformed = dto.WasTestPerformed.ToUpper() == Constants.CONST_NENHUM_VALUE ? null : dto.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE,
                UserDiseaseTests = dto.WasTestPerformed.ToUpper() == Constants.CONST_SIM_VALUE ? new List<UserDiseaseTest> { new UserDiseaseTest(dto.UserId, dto.ResultBool, dto.TestDate.Value, dto.ResultDate, null) } : null,
                UserVaccines = UserVaccine.CreateListUserVaccine(dto),
                ImportedFileId = importedFileId
            };

        public static UserDetails CreateUserDetail(AdminDto dto)
            => new()
            {
                Id = dto.Id.Value,
                HealthUnitId = dto.HealthUnitId
            };

        public void ChangeUserDetail(AdminDto dto) => HealthUnitId = dto.HealthUnitId;

        public bool CanEditCitizenFields(CitizenEditDto dto, AccessControlDTO accessControl)
        {
            if (accessControl.Profile == EProfileKey.government.ToString() || accessControl.Profile == EProfileKey.healthUnit.ToString())
                return Bond == dto.Bond
                && WasCovidInfected == dto.WasCovidInfected
                && PriorityGroupId == dto.PriorityGroupId;

            return accessControl.Profile == EProfileKey.admin.ToString();
        }
    }
}
