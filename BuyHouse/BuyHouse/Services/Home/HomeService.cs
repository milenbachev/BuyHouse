namespace BuyHouse.Services.Home
{
    using BuyHouse.Data;
    using BuyHouse.Models.Home;
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
    }
}
