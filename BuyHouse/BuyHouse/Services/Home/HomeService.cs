namespace BuyHouse.Services.Home
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuyHouse.Data;
    using BuyHouse.Models.Home;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly BuyHouseDbContext data;
        private readonly IMapper mapper;

        public HomeService(BuyHouseDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IndexViewModel GetCounts()
        {
            var totalProperties = this.data.Properties.Count(x => x.IsPublic);
            var totalAgent = this.data.Agents.Count();
            var totalCategory = this.data.Categories.Count();
            var totalUsers = this.data.Users.Count();

            return (new IndexViewModel
            {
                TotalAgents = totalAgent,
                TotalProperties = totalProperties,
                TotalCategory = totalCategory,
                TotalUsers = totalUsers
            });
        }

        public List<PropertyViewModel> GetLast()
        {
            return this.data
                .Properties
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ProjectTo<PropertyViewModel>(this.mapper.ConfigurationProvider)
                .ToList();     
        }
    }
}
