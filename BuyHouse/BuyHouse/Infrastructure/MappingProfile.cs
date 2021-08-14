namespace BuyHouse.Infrastructure
{
    using AutoMapper;
    using BuyHouse.Areas.Admin.Services.Categories;
    using BuyHouse.Areas.Admin.Services.Users;
    using BuyHouse.Data.Models;
    using BuyHouse.Models.Agents;
    using BuyHouse.Models.Home;
    using BuyHouse.Models.Issues;
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.Issues;
    using BuyHouse.Services.Properties;
    using BuyHouse.Services.Properties.Models;

    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<PropertyDetailsServiceModel, PropertyFormModel>();
            this.CreateMap<IssuesServiceListModel, CreateIssueFormModel>();
            this.CreateMap<AgentsServiceListModel, AgentFormModel>();

            this.CreateMap<Property, PropertyDetailsServiceModel>()
                .ForMember(x => x.UserId, cfg => cfg.MapFrom(x => x.Agent.UserId));

            this.CreateMap<Property, PropertyServiceListModel>();
            this.CreateMap<Category, PropertyCategoryServiceModel>();
            this.CreateMap<Category, CategoryServiceListModel>();
            this.CreateMap<Issue, IssuesServiceListModel>();
            this.CreateMap<Agent, AgentsServiceListModel>();
            this.CreateMap<Property, PropertyViewModel>();
            this.CreateMap<User, UserServiceListModel>();
        }
    }
}
