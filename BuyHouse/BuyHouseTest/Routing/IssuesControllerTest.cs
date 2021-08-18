namespace BuyHouseTest.Routing
{
    using BuyHouse.Controllers;
    using BuyHouse.Models.Issues;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class IssuesControllerTest
    {
        [Fact]
        public void GetCreateShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Issues/Create")
                .To<IssuesController>(x => x.Create());
        }

        [Fact]
        public void PostCreateShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Issues/Create")
                    .WithMethod(HttpMethod.Post))
                .To<IssuesController>(x => x.Create(
                    With.Any<CreateIssueFormModel>(),
                    With.Any<int>()));
        }

        [Fact]
        public void GetEditShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Issues/Edit/1")
                .To<IssuesController>(x => x.Edit(1));
        }

        [Fact]
        public void PostEditShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Issues/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<IssuesController>(x => x.Edit(
                    With.Any<int>(),
                    With.Any<CreateIssueFormModel>()));
        }    
    }
}
