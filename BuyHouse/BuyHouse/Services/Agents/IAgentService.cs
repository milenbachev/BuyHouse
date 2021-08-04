namespace BuyHouse.Services.Agents
{
    public interface IAgentService
    {
        int Create(
            string name,
            string phoneNumber,
            string description,
            string imageUrl,
            string city,
            string UserId);

        public bool IsAgent(string userId);

        public int? AgentsId(string userId);
    }
}
