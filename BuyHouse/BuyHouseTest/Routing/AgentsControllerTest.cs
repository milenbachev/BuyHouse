namespace BuyHouseTest.Routing
{
    using BuyHouse.Controllers;
    using BuyHouse.Models.Agents;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AgentsControllerTest
    {
        [Fact]
        public void GetCreateShouldBeMapped() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Agents/Create")
                .To<AgentsController>(x => x.Create());
        }

        [Fact]
        public void PostCreateShouldBeMapped() 
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Agents/Create")
                    .WithMethod(HttpMethod.Post))
                .To<AgentsController>(x => x.Create(With.Any<AgentFormModel>()));
        }

        [Fact]
        public void GetAllShouldBeMapped() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Agents/All")
                .To<AgentsController>(x => x.All(With.Any<AllAgentModel>()));
        }

        [Fact]
        public void GetDetailsShouldBeMapped() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Agents/Details/1")
                .To<AgentsController>(x => x.Details(1));
        }
    }
}
