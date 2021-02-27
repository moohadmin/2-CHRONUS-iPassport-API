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
        public static UserManager<Users> Create()
        {
            Mock<IUserStore<Users>> userStoreMock = new Mock<IUserStore<Users>>();

            userStoreMock.Setup(x => x.FindByIdAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.FromResult(UserSeed.GetUsers()));

            userStoreMock.Setup(x => x.UpdateAsync(It.IsAny<Users>(), CancellationToken.None))
                .Returns(Task.FromResult(IdentityResult.Success));

            return new UserManager<Users>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
