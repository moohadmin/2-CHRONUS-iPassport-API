using iPassport.Domain.Entities.Authentication;
using iPassport.Test.Seeds;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace iPassport.Test.Settings.Factories
{
    public static class UserManagerFactory
    {
        public static Mock<UserManager<Users>> CreateMock()
        {
            Mock<IUserStore<Users>> userStoreMock = new Mock<IUserStore<Users>>();
            return new Mock<UserManager<Users>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
