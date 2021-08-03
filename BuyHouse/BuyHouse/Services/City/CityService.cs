namespace BuyHouse.Services.City
{
    using BuyHouse.Data;
    using BuyHouse.Services.Properties;
    using System.Collections.Generic;
    using System.Linq;

    public class CityService :ICityService
    {
        private readonly BuyHouseDbContext data;

        public CityService(BuyHouseDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<PropertyCityServiceModel> AllCity()
        {
            return this.data
                .Cities
                .Select(x => new PropertyCityServiceModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        public bool CityExist(int cityId)
        {
            return this.data
                .Cities
                .Any(x => x.Id == cityId);
        }
    }
}
