﻿namespace BuyHouse.Models.Properties
{
    using BuyHouse.Services.Properties;
    using System.Collections.Generic;

    public class AllPropertyModel
    {
        public const int PropertyPerPage = 3;

        public int CurentPage { get; set; } = 1;

        public string Category { get; set; }

        public string Transaction { get; set; }

        public string City { get; set; }

        public string Construction { get; set; }

        public IEnumerable<string> Constructions { get; set; }

        public IEnumerable<string> Cities { get; set; }

        public IEnumerable<string> Transactions { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<PropertyServiceModel> Properties { get; set; }
    }
}