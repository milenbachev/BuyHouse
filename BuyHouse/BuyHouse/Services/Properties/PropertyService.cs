namespace BuyHouse.Services.Properties
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Services.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PropertyService : IPropertyService
    {
        private readonly BuyHouseDbContext data;

        public PropertyService(BuyHouseDbContext data) 
        {
            this.data = data;
        }

        public int Create(
            int area,
            string title,
            int year,
            int? floor, 
            int? floors, 
            int? bedRoom, 
            int? bath, 
            int price, 
            string imageUrl, 
            string description, 
            int categoryId, 
            string typeOfTransaction, 
            string city,
            string construction,
            int? agentId)

        {
            var property = new Property
            {
                Area = area,
                Title = title,
                Year = year,
                Floor = floor,
                Floors = floors,
                BedRoom = bedRoom,
                Bath = bath,
                Price = price,
                ImageUrl = imageUrl,
                Description = description,
                TypeOfTransaction = typeOfTransaction,
                Construction = construction,
                City = city,
                CategotyId = categoryId,
                AgentId = agentId,

            };

            this.data.Properties.Add(property);
            this.data.SaveChanges();

            return property.Id;
        }

        public PropertyServiceQueryModel All(
            string transaction,
            string city,
            string construction, 
            int curentPage, 
            int propertyPerPage)
        {
            var propertyQuery = this.data.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
            {
                propertyQuery = propertyQuery
                    .Where(x => x.City == city);
            };
            if (!string.IsNullOrWhiteSpace(construction))
            {
                propertyQuery = propertyQuery
                    .Where(x => x.Construction == construction);
            };
            if (!string.IsNullOrWhiteSpace(transaction))
            {
                propertyQuery = propertyQuery
                    .Where(x => x.TypeOfTransaction == transaction);
            };

            var properties = this.GetProperties(propertyQuery
                .Skip((curentPage - 1) * propertyPerPage)
                .Take(propertyPerPage));

            var totalProperty = propertyQuery.Count();

            return new PropertyServiceQueryModel
            {
                TotalProperties = totalProperty,
                CurentPage = curentPage,
                PropertyPerPage = propertyPerPage,
                Properties = properties
            };
        }

        public IEnumerable<PropertyCategoryServiceModel> AllCategory()
        {
            return this.data
                .Categories
                .Select(x => new PropertyCategoryServiceModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }


        public bool CategoryExist(int categoryId)
        {
            return this.data
                .Categories
                .Any(x => x.Id == categoryId);
        }
        private IEnumerable<PropertyServiceListModel> GetProperties(IQueryable<Property> properties) 
        {
            return properties
                .Select(x => new PropertyServiceListModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Area = x.Area,
                    CategoryId = x.CategotyId,
                    CategoryName = x.Category.Name,
                    Construction = x.Construction,
                    City = x.City,
                    Transaction = x.TypeOfTransaction
                })
                .ToList();
        }

        public IEnumerable<string> AllCity()
        {
            var cities = this.data
                .Properties
                .Select(x => x.City)
                .Distinct()
                .ToList();

            return cities;
        }

        public IEnumerable<string> AllConstruction()
        {
            var constructions = this.data
                .Properties
                .Select(x => x.Construction)
                .Distinct()
                .ToList();

            return constructions;
        }

        public IEnumerable<string> AllTransaction()
        {
            var transacions = this.data
                .Properties
                .Select(x => x.TypeOfTransaction)
                .Distinct()
                .ToList();

            return transacions;
        }
    }
}
