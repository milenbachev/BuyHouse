namespace BuyHouse.Services.Agents
{
    using BuyHouse.Models.Agents;

    public interface IAgentService
    {
        int Create(
            string name,
            string phoneNumber,
            string description,
            string imageUrl,
            string city,
            string UserId);

        AgentsServiceQueryModel All(int curentPage, int agentPerPage);

        AgentsServiceListModel Details(int id);

        public bool IsAgent(string userId);

        public int AgentsId(string userId);
    }
}
