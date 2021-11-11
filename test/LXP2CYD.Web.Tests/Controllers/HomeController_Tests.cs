using System.Threading.Tasks;
using LXP2CYD.Models.TokenAuth;
using LXP2CYD.Web.Controllers;
using Shouldly;
using Xunit;

namespace LXP2CYD.Web.Tests.Controllers
{
    public class HomeController_Tests: LXP2CYDWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}