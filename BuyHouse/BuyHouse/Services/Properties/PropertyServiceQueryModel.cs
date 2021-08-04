namespace BuyHouse.Services.Properties
{
    using System.Collections.Generic;
    public class PropertyServiceQueryModel
    {
        public int CurentPage { get; set; }

        public int PropertyPerPage { get; set; }

        public int TotalProperties { get; set; }

        public IEnumerable<PropertyServiceListModel> Properties { get; set; }
    }
}
