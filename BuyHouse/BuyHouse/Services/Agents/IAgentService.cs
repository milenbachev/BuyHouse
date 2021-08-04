namespace BuyHouse.Services.Agents
{
    public interface IAgentService
    {
        int Create(
            string name,
            string phoneNumber,
            string description,
            string imageUrl,
            int cityId,
            string UserId);

        public bool IsAgent(string userId);

        public int? AgentsId(string userId);
    }
}
