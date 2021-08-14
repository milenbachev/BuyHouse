namespace BuyHouse.Areas.Admin.Services.Agents
{
    using BuyHouse.Data;
    using System.Linq;

    public class AgentAdminService : IAgentAdminService
    {
        private readonly BuyHouseDbContext data;

        public AgentAdminService(BuyHouseDbContext data) 
        {
            this.data = data;
        }

        public bool Edit(
            int id,
            string name, 
            string phoneNumber,
            string description,
            string imageUrl, 
            string city)
        {
            var agent = this.data.Agents.Find(id);

            if (agent == null) 
            {
                return false;
            }

            agent.Name = name;
            agent.PhoneNumber = phoneNumber;
            agent.Description = description;
            agent.ImageUrl = imageUrl;
            agent.City = city;

            this.data.SaveChanges();

            return true;
        }
        public bool Delete(int id)
        {
            var agent = this.data
                .Agents
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (agent == null) 
            {
                return false;
            }

            var user = this.data
                .Users
                .FirstOrDefault(x => x.Id == agent.UserId);

            user.IsAgent = false;

            this.data.Agents.Remove(agent);
            this.data.SaveChanges();

            return true;
        }
    }
}
