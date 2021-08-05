namespace BuyHouse.Models.Agents
{
    using BuyHouse.Services.Agents;
    using System.Collections.Generic;

    public class AllAgentModel :PageViewModel
    {
        public int TotalAgents { get; set; }

        public IEnumerable<AgentsServiceListModel> Agents { get; set; }
    }
}
