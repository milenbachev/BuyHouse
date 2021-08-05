namespace BuyHouse.Services.Agents
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Models.Agents;
    using BuyHouse.Services.Properties;
    using System.Collections.Generic;
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

        public AgentsServiceQueryModel All(int curentPage, int agentPerPage)
        {
            var agentQuery = this.data.Agents.AsQueryable();

            var agents = this.GetAgents(agentQuery
                .Skip((curentPage - 1) * agentPerPage)
                .Take(agentPerPage));
                
              
            var totalAgent = agentQuery.Count();

            return new AgentsServiceQueryModel
            {
                TotalAgents = totalAgent,
                CurentPage = curentPage,
                AgentPerPage = agentPerPage,
                Agents = agents
            };
        }

        public AgentsServiceListModel Details(int id)
        {
            return this.data
                .Agents
                .Where(x => x.Id == id)
                .Select(x => new AgentsServiceListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    City = x.City,
                    UserId = x.UserId,
                })
                .FirstOrDefault();
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
        private IEnumerable<AgentsServiceListModel> GetAgents(IQueryable<Agent> agents)
        {
            return agents
                .Select(x => new AgentsServiceListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Description = x.Description,
                    City = x.City,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId
                })
                .ToList();
        }
    }
}
