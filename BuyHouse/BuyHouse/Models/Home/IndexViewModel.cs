namespace BuyHouse.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProperties { get; set; }

        public int TotalAgents { get; set; }

        public List<PropertyViewModel> Properties { get; set; }
    }
}
