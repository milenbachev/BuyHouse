namespace BuyHouse.Services.Properties
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Services.Properties;
    using BuyHouse.Services.Properties.Models;
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

        public PropertyDetailsServiceModel Details(int id)
        {
            return this.data
                .Properties
                .Where(x => x.Id == id)
                .Select(x => new PropertyDetailsServiceModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    Title = x.Title,
                    Floor = x.Floor,
                    Floors = x.Floors,
                    BedRoom = x.BedRoom,
                    Bath = x.Bath,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    City = x.City,
                    Construction = x.Construction,
                    Transaction = x.TypeOfTransaction,
                    CategoryId = x.CategotyId,
                    CategoryName = x.Category.Name,
                    Year = x.Year,
                    AgentId = (int)x.AgentId,
                    UserId = x.Agent.UserId,
                    AgentName = x.Agent.Name
                })
                .FirstOrDefault();
        }
        public bool Edit(
            int id,
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
            string construction)
        {
            var property = this.data.Properties.Find(id);

            if (property == null) 
            {
                return false;
            }

            property.Area = area;
            property.Title = title;
            property.Year = year;
            property.Floor = floor;
            property.Floors = floors;
            property.BedRoom = bedRoom;
            property.Bath = bath;
            property.Price = price;
            property.ImageUrl = imageUrl;
            property.Description = description;
            property.CategotyId = categoryId;
            property.TypeOfTransaction = typeOfTransaction;
            property.City = city;
            property.Construction = construction;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var property = this.data
                .Properties
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (property == null) 
            {
                return false;
            }

            this.data.Properties.Remove(property);
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<PropertyServiceListModel> AgentProperties(string userId)
        {
            return this.GetProperties(this.data
                .Properties
                .Where(x => x.Agent.UserId == userId));
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

        public bool AgentIsCreate(int propertyId, int agentId)
        {
            return this.data
                .Properties
                .Any(x => x.Id == propertyId && x.AgentId == agentId);
        }
    }
}
