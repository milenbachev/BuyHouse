namespace BuyHouse.Services.Agents
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using System.Linq;
    public class AgentService : IAgentService
    {
        private readonly BuyHouseDbContext data;

        public AgentService(BuyHouseDbContext data) 
        {
            this.data = data;
        }

        public int Create(
            string name, 
            string phoneNumber, 
            string description, 
            string imageUrl, 
            string city,
            string userId)
        {

            var agent = new Agent
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Description = description,
                ImageUrl = imageUrl,
                City = city,
                UserId = userId
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
