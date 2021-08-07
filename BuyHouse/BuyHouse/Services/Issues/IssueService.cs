namespace BuyHouse.Services.Issues
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;

    public class IssueService : IIssueService
    {
        private readonly BuyHouseDbContext data;

        public IssueService(BuyHouseDbContext data) 
        {
            this.data = data;
        }

        public int Create(int propertyId,  string userId, string descriptionIssue)
        {

            var issue = new Issue
            {
                PropertyId = propertyId,
                UserId = userId,
                DescriptionIssue = descriptionIssue,
                CreateOn = System.DateTime.UtcNow
            };

            this.data.Issues.Add(issue);
            this.data.SaveChanges();

            return issue.Id;
 
        }
    }
}
