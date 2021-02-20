using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Services;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Test.Seeds;
using iPassport.Test.Settings.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        Mock<IUserDetailsRepository> _mockRepository;
        Mock<IPlanRepository> _planMockRepository;
        IUserService _service;
        IMapper _mapper;
        IHttpContextAccessor _accessor;        

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _accessor = HttpContextAccessorFactory.Create();
            _mockRepository = new Mock<IUserDetailsRepository>();
            _planMockRepository = new Mock<IPlanRepository>();
            

            _service = new UserService(_mockRepository.Object, _planMockRepository.Object, _mapper, _accessor, null, null);
        }

        [TestMethod]
        public void AssociatePlan()
        {
            var userSeed = UserSeed.GetUserDetails();
            var planSeed = PlanSeed.GetPlans().FirstOrDefault();
            var mockRequest = Guid.NewGuid();

            // Arrange
            _mockRepository.Setup(r => r.Update(It.IsAny<UserDetails>()));
            _mockRepository.Setup(r => r.FindWithUser(It.IsAny<Guid>()).Result).Returns(userSeed);
            _planMockRepository.Setup(r => r.Find(It.IsAny<Guid>()).Result).Returns(planSeed);
            
            // Act
            var result = _service.AssociatePlan(mockRequest);

            // Assert
            _mockRepository.Verify(a => a.Update(It.IsAny<UserDetails>()), Times.Once);
            _mockRepository.Verify(a => a.FindWithUser(It.IsAny<Guid>()), Times.Once);
            _planMockRepository.Verify(a => a.Find(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
        }

        [TestMethod]
        public void GetUserPlan()
        {
            var userSeed = UserSeed.GetUserDetails();
            var planSeed = PlanSeed.GetPlans().FirstOrDefault();
            
            // Arrange
            _mockRepository.Setup(r => r.FindWithUser(It.IsAny<Guid>()).Result).Returns(userSeed);
            _planMockRepository.Setup(r => r.Find(It.IsAny<Guid>()).Result).Returns(planSeed);

            // Act
            var result = _service.GetUserPlan();

            // Assert
            _mockRepository.Verify(a => a.FindWithUser(It.IsAny<Guid>()), Times.Once);
            _planMockRepository.Verify(a => a.Find(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(Task<ResponseApi>));
            Assert.IsNotNull(result.Result.Data);
            Assert.IsInstanceOfType(result.Result.Data, typeof(PlanViewModel));
        }
    }
}
