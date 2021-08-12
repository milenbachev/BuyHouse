namespace BuyHouse.Models.Properties
{
    using BuyHouse.Services.Properties.Models;
    using System.Collections.Generic;


    public class AllPropertyModel : PageViewModel
    {
        public int TotalProperties { get; set; }

        public string Transaction { get; set; }

        public string City { get; set; }

        public string Construction { get; set; }

        public IEnumerable<string> Constructions { get; set; }

        public IEnumerable<string> Cities { get; set; }

        public IEnumerable<string> Transactions { get; set; }

        public IEnumerable<PropertyServiceListModel> Properties { get; set; }
    }
}
