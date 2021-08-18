namespace BuyHouseTest.Controllers
{
    using BuyHouse.Controllers;
    using BuyHouse.Data.Models;
    using BuyHouse.Models.Issues;
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Properties.Models;
    using BuyHouseTest.Data;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class IssuesControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUserAndReturnView() 
        {
            MyController<IssuesController>
                .Instance()
                .Calling(x => x.Create())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData(1, "samo description")]
        public void PostCreateShouldBeForAuthorizedUserAndReturnRedirectWithValidModel(
            int id,
            string description)
        {
            MyController<IssuesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Create(new CreateIssueFormModel
                {
                    DescriptionIssue = description,
                    CreateOn = System.DateTime.UtcNow
                },
                id
                ))
                .ShouldHave()
                .ActionAttributes(atributes => atributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Issue>(issue => issue
                        .Any(x =>
                            x.Id == id &&
                            x.DescriptionIssue == description
                            )))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                .To<PropertiesController>(x => x.All(With.Any<AllPropertyModel>())));
        }
        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState() 
        {
            MyController<IssuesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Create(With.Default<CreateIssueFormModel>(),
                    With.Default<int>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<CreateIssueFormModel>());
        }

       [Fact]
        public void EditShouldReturnBeAuthorizedUser() 
        {
            MyController<IssuesController>
                  .Instance(controller => controller
                      .WithUser()
                      .WithData(IssuesTestData.GetIssue(1, 1, "userTest", "some description")))
                  .Calling(x => x.Edit(1))
                  .ShouldHave()
                  .ActionAttributes(attributes => attributes
                      .RestrictingForAuthorizedRequests())
                  .AndAlso()
                  .ShouldReturn()
                  .View();
        }

        [Theory]
        [InlineData(1,"some description")]
        public void PosrEditShouldBeAuthorizedUserAndReturnRedirectToViewWithValidModel(
            int id,
            string description) 
        {
            MyController<IssuesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Edit(id,
                    new CreateIssueFormModel
                    {
                        DescriptionIssue = description,
                        CreateOn = System.DateTime.UtcNow
                    }))
                .ShouldHave()
                .ActionAttributes(attribut => attribut
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Issue>(issue => issue
                        .Any(x =>
                            x.Id == id &&
                            x.DescriptionIssue == description)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PropertiesController>(x => x.AgentProperties()));
        }
    }
}
