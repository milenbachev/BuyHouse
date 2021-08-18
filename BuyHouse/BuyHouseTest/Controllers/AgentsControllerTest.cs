namespace BuyHouseTest.Controllers
{
    using BuyHouse.Controllers;
    using BuyHouse.Data.Models;
    using BuyHouse.Models.Agents;
    using BuyHouse.Services.Agents;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class AgentsControllerTest
    {
        [Fact]
        public void CreateGetShouldBeForAuthorizedUsersAndReturnView() 
        {
            MyController<AgentsController>
                .Instance()
                .Calling(x => x.Create())
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData("Agent name", "089989898", "Samo tests", "", "Varna")]
        public void CreatePostShouldHaveRestrictionsForHttpPostAndAuthorizedUser(
            string name,
            string phoneNumber,
            string description,
            string image,
            string city) 
        {
            MyController<AgentsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Create(new AgentFormModel
                { 
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Description = description,
                    ImageUrl = image,
                    City = city
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState() 
        {
            MyController<AgentsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Create(With.Default<AgentFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<AgentFormModel>());
        }

        [Fact]
        public void DetailsShouldReturnNotFoundInvalidAgentId ()
        {
            MyController<AgentsController>
                .Calling(x => x.Details(int.MaxValue))
                .ShouldReturn()
                .NotFound();
        }
    }
}
