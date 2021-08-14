namespace BuyHouse.Areas.Admin.Services.Agents
{
    public interface IAgentAdminService
    {
        bool Edit(int id,
            string name,
            string phoneNumber,
            string description,
            string imageUrl,
            string city);

        bool Delete(int id);
    }
}
