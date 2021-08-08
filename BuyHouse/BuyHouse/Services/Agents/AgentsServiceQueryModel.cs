namespace BuyHouse.Models.Agents
{
    using BuyHouse.Services.Agents;
    using System.Collections.Generic;

    public class AgentsServiceQueryModel
    {
        public int CurentPage { get; set; } 

        public int TotalAgents { get; set; }

        public int AgentPerPage { get; set; }

       public IEnumerable<AgentsServiceListModel> Agents { get; set; }
    }
}
