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
            int? floor, 
            int? floors, 
            int? bedRoom, 
            int? bath, 
            int price, 
            string imageUrl, 
            string description, 
            int categoryId, 
            int typeOfTransactionId, 
            int cityId, 
            int? agentId, 
            int constructionId)
        {
            var property = new Property
            {
                Area = area,
                Floor = floor,
                Floors = floors,
                BedRoom = bedRoom,
                Bath = bath,
                Price = price,
                ImageUrl = imageUrl,
                Description = description,
                CategotyId = categoryId,
                TypeOfTransactionId = typeOfTransactionId,
                CityId = cityId,
                AgentId = agentId,
                ConstructionId = constructionId
            };

            this.data.Properties.Add(property);
            this.data.SaveChanges();

            return property.Id;
        }

        public PropertyServiceQueryModel All(
            string category,
            string city, 
            string transaction, 
            string construction, 
            int curentPage, 
            int propertyPerPage)
        {
            var propertyQuery = this.data.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category)) 
            {
                propertyQuery = propertyQuery
                    .Where(x => x.Category.Name == category);
            }
            if (!string.IsNullOrWhiteSpace(city)) 
            {
                propertyQuery = propertyQuery
                    .Where(x => x.City.Name == city);
            }
            if (!string.IsNullOrWhiteSpace(transaction)) 
            {
                propertyQuery = propertyQuery
                    .Where(x => x.TypeOfTransaction.Name == transaction);
            }
            if (!string.IsNullOrWhiteSpace(construction)) 
            {
                propertyQuery = propertyQuery
                    .Where(x => x.Construction.Name == construction);
            }

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

        public IEnumerable<PropertyTransactionServiceModel> AllTransaction()
        {
            return this.data
                .TypeOfTransactions
                .Select(x => new PropertyTransactionServiceModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        public IEnumerable<PropertyConstructionServiceModel> AllConstruction()
        {
            return this.data
                .Constructions
                .Select(x => new PropertyConstructionServiceModel
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

        public bool TransactionExist(int transactionId)
        {
            return this.data
                .TypeOfTransactions
                .Any(x => x.Id == transactionId);
        }

        public bool ConstructionExist(int constructionId)
        {
            return this.data
                .Constructions
                .Any(x => x.Id == constructionId);
        }

        private IEnumerable<PropertyServiceModel> GetProperties(IQueryable<Property> properties) 
        {
            return properties
                .Select(x => new PropertyServiceModel
                {
                    Id = x.Id,
                    BedRoom = x.BedRoom,
                    Floor = x.Floor,
                    Floors = x.Floors,
                    ImageUrl = x.ImageUrl,
                    Contruction = x.Construction.Name
                })
                .ToList();
        }
    }
}
