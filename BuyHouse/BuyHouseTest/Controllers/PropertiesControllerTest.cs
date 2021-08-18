namespace BuyHouseTest.Controllers
{
    using BuyHouse.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PropertiesControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizedUserAndRedirectIfUserIsNotAgent() 
        { 
            MyController<PropertiesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Add())
                .ShouldHave()
                .ActionAttributes(attribites => attribites
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Redirect();
        }
    }
}
