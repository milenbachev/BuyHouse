namespace BuyHouse.Services.City
{
    using BuyHouse.Services.Properties;
    using System.Collections.Generic;

    public interface ICityService
    {
        IEnumerable<PropertyCityServiceModel> AllCity();

        bool CityExist(int cityId);
    }
}
