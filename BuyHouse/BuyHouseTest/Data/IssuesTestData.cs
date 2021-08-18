namespace BuyHouseTest.Data
{
    using BuyHouse.Data.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public static class IssuesTestData
    {
        public static List<Issue>GetIssue(int count, int propertyId, string userId, string descriptionIssue) 
        {
            var user = new User
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };

            var issue = Enumerable.Range(1, count)
                .Select(x => new Issue
                {
                    PropertyId = propertyId,
                    UserId = userId,
                    DescriptionIssue = descriptionIssue,
                    CreateOn = System.DateTime.UtcNow

                })
                .ToList();

            return issue;

        }
    }
}
