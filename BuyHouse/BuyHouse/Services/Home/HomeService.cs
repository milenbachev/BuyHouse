namespace BuyHouse.Services.Home
{
    using BuyHouse.Data;
    using BuyHouse.Models.Home;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly BuyHouseDbContext data;

        public HomeService(BuyHouseDbContext data)
        {
            this.data = data;
        }

        public IndexViewModel GetCounts()
        {
            var totalProperties = this.data.Properties.Count();
            var totalAgent = this.data.Agents.Count();

            return (new IndexViewModel
            {
                TotalAgents = totalAgent,
                TotalProperties = totalProperties
            });
        }

        public List<PropertyViewModel> GetRandom()
        {
            return this.data
                .Properties
                .OrderByDescending(x => x.Id)
                .Take(3)
                .Select(x => new PropertyViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    City = x.City,
                })
                .ToList();
                

        }
    }
}
