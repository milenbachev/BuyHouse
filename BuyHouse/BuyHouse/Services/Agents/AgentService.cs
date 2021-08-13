namespace BuyHouse.Services.Agents
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Models.Agents;
    using System.Linq;
    public class AgentService : IAgentService
    {
        private readonly BuyHouseDbContext data;
        private readonly IMapper mapper;

        public AgentService(BuyHouseDbContext data, IMapper mapper) 
        {
            this.data = data;
            this.mapper = mapper;
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
                UserId = userId,
            };

            var user = this.data
                .Users
                .FirstOrDefault(x => x.Id == agent.UserId);

            user.IsAgent = true;

            this.data.Agents.Add(agent);
            this.data.SaveChanges();

            return agent.Id;
        }

        public AgentsServiceQueryModel All(int curentPage, int agentPerPage)
        {
            var agentQuery = this.data.Agents.AsQueryable();

            var agents = this.data
                .Agents
                .ProjectTo<AgentsServiceListModel>(this.mapper.ConfigurationProvider)
                .Skip((curentPage - 1) * agentPerPage)
                .Take(agentPerPage)
                .ToList(); 
              
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
                .ProjectTo<AgentsServiceListModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public int AgentsId(string userId)
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
