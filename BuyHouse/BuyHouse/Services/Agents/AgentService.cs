namespace BuyHouse.Services.Agents
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Services.City;
    using System.Linq;
    public class AgentService : IAgentService
    {
        private readonly BuyHouseDbContext data;
        private readonly ICityService cityService;

        public AgentService(BuyHouseDbContext data, ICityService cityService) 
        {
            this.data = data;
            this.cityService = cityService;
        }

        public int Create(
            string name, 
            string phoneNumber, 
            string description, 
            string imageUrl, 
            int cityId)
        {
            var agent = new Agent
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Description = description,
                ImageUrl = imageUrl,
                CityId = cityId
            };

            this.data.Agents.Add(agent);
            this.data.SaveChanges();

            return agent.Id;
        }

        public int? AgentsId(string userId)
        {
            return this.data
                .Agents
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public bool IsAgent(string userId)
        {
            return this.data
                .Agents
                .Any(x => x.UserId == userId);
        }
    }
}
