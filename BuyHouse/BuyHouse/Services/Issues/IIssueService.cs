namespace BuyHouse.Services.Issues
{
    using System;
    using System.Collections.Generic;

    public interface IIssueService
    {
        int Create(int propertyId, string userId, string descriptionIssue);

        bool Edit(int id, string description, DateTime createOn);

        bool Delete(int id);

        IssuesServiceListModel Details(int id);

        IEnumerable<IssuesServiceListModel> AllIssue(int id);
    }
}
