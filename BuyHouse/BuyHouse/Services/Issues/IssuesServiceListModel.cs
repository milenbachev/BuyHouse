namespace BuyHouse.Services.Issues
{
    using System;
    public class IssuesServiceListModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string DescriptionIssue { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
