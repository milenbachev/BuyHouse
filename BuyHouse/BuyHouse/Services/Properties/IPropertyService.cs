namespace BuyHouse.Services.Properties
{
    using BuyHouse.Services.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPropertyService
    {
        int Create(
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
            int constructionId);


        IEnumerable<PropertyCategoryServiceModel> AllCategory();

        IEnumerable<PropertyTransactionServiceModel> AllTransaction();

        IEnumerable<PropertyCityServiceModel> AllCity();

        IEnumerable<PropertyConstructionServiceModel> AllConstruction();

        bool CategoryExist(int categoryId);

        bool TransactionExist(int transactionId);

        bool CityExist(int cityId);

        bool ConstructionExist(int constructionId);
    }
}
