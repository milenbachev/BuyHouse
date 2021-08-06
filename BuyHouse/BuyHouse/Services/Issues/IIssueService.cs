namespace BuyHouse.Services.Issues
{
    public interface IIssueService
    {
        int Create(int propertyId, string userId, string descriptionIssue);
    }
}
