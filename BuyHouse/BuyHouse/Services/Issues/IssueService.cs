﻿namespace BuyHouse.Services.Issues
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public IEnumerable<IssuesServiceListModel> AllIssue(int id)
        {
            var issues = this.data
                .Issues
                .Where(x => x.PropertyId == id)
                .Select(x => new IssuesServiceListModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    DescriptionIssue = x.DescriptionIssue,
                    CreateOn = x.CreateOn
                })
                .ToList();

            return issues;
        }

        public bool Edit(int id,  string description, DateTime createOn)
        {
            var issue = this.data.Issues.Find(id);

            if (issue == null) 
            {
                return false;
            }

            issue.DescriptionIssue = description;
            issue.CreateOn = createOn;

            this.data.SaveChanges();

            return true;
        }

        public IssuesServiceListModel Details(int id)
        {
            var issue = this.data
                .Issues
                .Where(x => x.Id == id)
                .Select(x => new IssuesServiceListModel
                {
                    DescriptionIssue = x.DescriptionIssue,
                    CreateOn = System.DateTime.UtcNow
                })
                .FirstOrDefault();

            return issue;
        }

        public bool Delete(int id)
        {
            var issue =this.data
                .Issues
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (issue == null) 
            {
                return false;
            }

            this.data.Issues.Remove(issue);
            this.data.SaveChanges();

            return true;
        }
    }
}
