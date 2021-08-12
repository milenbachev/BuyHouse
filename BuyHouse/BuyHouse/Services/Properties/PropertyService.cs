namespace BuyHouse.Services.Properties
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Services.Properties.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class PropertyService : IPropertyService
    {
        private readonly BuyHouseDbContext data;
        private readonly IMapper mapper;

        public PropertyService(BuyHouseDbContext data, IMapper mapper) 
        {
            this.data = data;
            this.mapper = mapper;
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
            int agentId)

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
                CategoryId = categoryId,
                IsPublic = false,
                AgentId = agentId,

            };

            this.data.Properties.Add(property);
            this.data.SaveChanges();

            return property.Id;
        }

        public PropertyServiceQueryModel All(
            string transaction = null,
            string city = null,
            string construction = null, 
            int curentPage = 1, 
            int propertyPerPage = 5,
            bool publicOnly = true)
        {
            var propertyQuery = this.data.Properties
                .Where(x => publicOnly ? x.IsPublic : true);

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
                .ProjectTo<PropertyDetailsServiceModel>(this.mapper.ConfigurationProvider)
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
            string construction,
            bool isPublic)
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
            property.CategoryId = categoryId;
            property.TypeOfTransaction = typeOfTransaction;
            property.City = city;
            property.Construction = construction;
            property.IsPublic = isPublic;

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

        public void Approve(int id)
        {
            var property = this.data.Properties.Find(id);

            property.IsPublic = !property.IsPublic;

            this.data.SaveChanges();
        }

        public IEnumerable<PropertyServiceListModel> AgentProperties(string userId)
        {
            return this.GetProperties(this.data
                .Properties
                .Where(x => x.Agent.UserId == userId));
        }

        public IEnumerable<PropertyServiceListModel> CurentAgentProperties(int id)
        {
            return this.GetProperties(this.data
                .Properties
                .Where(x => x.AgentId == id && x.IsPublic));
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

        private IEnumerable<PropertyServiceListModel> GetProperties(IQueryable<Property> properties) 
        {
            return properties
                .ProjectTo<PropertyServiceListModel>(this.mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
